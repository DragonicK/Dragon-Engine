using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Core.Model.Mailing;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Characters;
using Dragon.Game.Repository;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class MailingManager {
    public DatabaseService? DatabaseService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly ICharacterDatabase CharacterDatabase;

    public MailingManager(IServiceInjector injector) {
        injector.Inject(this);

        CharacterDatabase = new CharacterDatabase(Configuration!, DatabaseService!.DatabaseFactory!);
    }

    public void ProcessMailing(IPlayer player, CharacterMail mail, string name, int index, int amount) {
        var receiver = GetPlayerRepository().FindByName(name);
        var sender = GetPacketSender();

        if (receiver is not null) {
            CreateMailToOnlinePlayer(sender, player, mail, receiver, index, amount);
        }
        else {
            CreateMailToOfflinePlayer(sender, player, mail, name, index, amount);
        }
    }

    private void CreateMailToOnlinePlayer(IPacketSender sender, IPlayer player, CharacterMail mail, IPlayer receiver, int index, int amount) {
        var step = CreateMail(player, mail, index, amount);

        if (step != MailingOperationCode.Sended) {
            sender!.SendAlertMessage(player, GetAlertMessageType(step), MenuResetType.None);
        }
        else {
            if (CouldAttachCurrencyAndItem(player, mail, index, amount)) {
                sender.SendCurrencyUpdate(player, CurrencyType.Gold);
                sender!.SendInventoryUpdate(player, index);

                receiver.Mails.Add(mail);

                sender.SendMail(receiver, mail);

                sender.SendMessage(SystemMessage.YouJustReceivedMail, QbColor.Gold, receiver);
            }
        }

        sender.SendMailOperationResult(player, step);
    }

    private async void CreateMailToOfflinePlayer(IPacketSender sender, IPlayer player, CharacterMail mail, string name, int index, int amount) {
        var step = CreateMail(player, mail, index, amount);

        if (step != MailingOperationCode.Sended) {
            sender.SendAlertMessage(player.Connection, GetAlertMessageType(step), MenuResetType.None);
        }
        else {
            var id = await CharacterDatabase.GetCharacterIdAsync(name);

            if (id != 0) {
                if (CouldAttachCurrencyAndItem(player, mail, index, amount)) {
                    mail.ReceiverCharacterId = id;

                    sender.SendCurrencyUpdate(player!, CurrencyType.Gold);
                    sender!.SendInventoryUpdate(player!, index);

                    await CharacterDatabase.SaveMailAsync(mail);
                }
            }
            else {
                step = MailingOperationCode.InvalidReceiver;
            }
        }

        sender.SendMailOperationResult(player, step);
    }

    private MailingOperationCode CreateMail(IPlayer player, CharacterMail mail, int index, int amount) {
        if (index > 0) {
            var inventory = player.Inventories.FindByIndex(index);

            if (inventory is null) {
                return MailingOperationCode.InvalidItem;
            }

            if (inventory is not null) {
                if (inventory.Bound) {
                    return MailingOperationCode.InvalidItem;
                }
            }

            mail.AttachItemFlag = inventory is not null;
        }

        mail.SendDate = DateTime.Now;
        mail.SenderCharacterId = player.Character.CharacterId;
        mail.SenderCharacterName = player.Character.Name;
        mail.AttachCurrencyReceiveFlag = false;
        mail.AttachItemReceiveFlag = false;

        if (mail.AttachCurrency > 0) {
            if (player.Currencies.Get(CurrencyType.Gold) < mail.AttachCurrency) {
                return MailingOperationCode.CurrencyIsNotEnough;
            }
        }

        CalculateExpirationDate(mail);

        return MailingOperationCode.Sended;
    }

    private void CalculateExpirationDate(CharacterMail mail) {
        mail.ExpireDate = mail.SendDate.AddDays(Configuration!.Mail.TimeLimitInDays);
    }

    private bool CouldAttachCurrencyAndItem(IPlayer player, CharacterMail mail, int index, int amount) {
        if (mail.AttachCurrency > 0) {
            if (!player.Currencies.Subtract(CurrencyType.Gold, mail.AttachCurrency)) {
                return false;
            }
        }

        if (index > 0) {
            var inventory = player.Inventories.FindByIndex(index);

            if (inventory is null) {
                return false;
            }

            if (inventory is not null) {
                if (amount > inventory.Value) {
                    amount = inventory.Value;
                }

                var attached = new CharacterMailAttachItem() {
                    ItemId = inventory.ItemId,
                    Value = amount,
                    Level = inventory.Level,
                    AttributeId = inventory.AttributeId,
                    UpgradeId = inventory.UpgradeId
                };

                mail.AttachItemFlag = true;
                mail.MailAttachItem = attached;

                inventory.Value -= amount;

                if (inventory.Value <= 0) {
                    inventory.Clear();
                }
            }
        }

        return true;
    }

    private AlertMessageType GetAlertMessageType(MailingOperationCode code) => code switch {
        MailingOperationCode.InvalidItem => AlertMessageType.InvalidItem,
        MailingOperationCode.CurrencyIsNotEnough => AlertMessageType.NotEnoughCurrency,
        MailingOperationCode.InvalidReceiver => AlertMessageType.InvalidRecipientName,
        _ => AlertMessageType.None
    };

    private IPlayerRepository GetPlayerRepository() {
        return ConnectionService!.PlayerRepository!;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}