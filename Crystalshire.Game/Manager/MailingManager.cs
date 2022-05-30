using Crystalshire.Database;

using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Mailing;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Characters;
using Crystalshire.Game.Repository;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Manager;

public class MailingManager {
    public IPlayer? Player { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IPlayerRepository? PlayerRepository { get; init; }
    public IDatabaseFactory? Factory { get; init; }

    public void ProcessMailing(CharacterMail mail, string name, int index, int amount) {
        var receiver = PlayerRepository!.FindByName(name);

        if (receiver is not null) {
            CreateMailToOnlinePlayer(mail, receiver, index, amount);
        }
        else {
            CreateMailToOfflinePlayer(mail, name, index, amount);
        }
    }

    private void CreateMailToOnlinePlayer(CharacterMail mail, IPlayer receiver, int index, int amount) {
        var step = CreateMail(mail, index, amount);

        if (step != MailingOperationCode.Sended) {
            PacketSender!.SendAlertMessage(Player!.GetConnection(), GetAlertMessageType(step), MenuResetType.None);
        }
        else {
            if (CouldAttachCurrencyAndItem(mail, index, amount)) {
                PacketSender!.SendCurrencyUpdate(Player!, CurrencyType.Gold);
                PacketSender!.SendInventoryUpdate(Player!, index);

                receiver.Mails.Add(mail);

                PacketSender.SendMail(receiver, mail);

                PacketSender!.SendMessage(SystemMessage.YouJustReceivedMail, QbColor.Gold, receiver);
            }
        }

        PacketSender!.SendMailOperationResult(Player!, step);
    }

    private async void CreateMailToOfflinePlayer(CharacterMail mail, string name, int index, int amount) {
        var step = CreateMail(mail, index, amount);

        if (step != MailingOperationCode.Sended) {
            PacketSender!.SendAlertMessage(Player!.GetConnection(), GetAlertMessageType(step), MenuResetType.None);
        }
        else {
            var database = new CharacterDatabase(Configuration!, Factory!);

            var id = await database.GetCharacterIdAsync(name);

            if (id != 0) {
                if (CouldAttachCurrencyAndItem(mail, index, amount)) {
                    mail.ReceiverCharacterId = id;

                    PacketSender!.SendCurrencyUpdate(Player!, CurrencyType.Gold);
                    PacketSender!.SendInventoryUpdate(Player!, index);

                    await database.SaveMailAsync(mail);
                }
            }
            else {
                step = MailingOperationCode.InvalidReceiver;
            }

            database.Dispose();
        }

        PacketSender!.SendMailOperationResult(Player!, step);
    }

    private MailingOperationCode CreateMail(CharacterMail mail, int index, int amount) {
        if (index > 0) {
            var inventory = Player!.Inventories.FindByIndex(index);

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
        mail.SenderCharacterId = Player!.Character.CharacterId;
        mail.SenderCharacterName = Player!.Character.Name;
        mail.AttachCurrencyReceiveFlag = false;
        mail.AttachItemReceiveFlag = false;

        if (mail.AttachCurrency > 0) {
            if (Player!.Currencies.Get(CurrencyType.Gold) < mail.AttachCurrency) {
                return MailingOperationCode.CurrencyIsNotEnough;
            }
        }

        CalculateExpirationDate(mail);

        return MailingOperationCode.Sended;
    }

    private void CalculateExpirationDate(CharacterMail mail) {
        mail.ExpireDate = mail.SendDate.AddDays(Configuration!.Mail.TimeLimitInDays);
    }

    private bool CouldAttachCurrencyAndItem(CharacterMail mail, int index, int amount) {
        if (mail.AttachCurrency > 0) {
            if (!Player!.Currencies.Subtract(CurrencyType.Gold, mail.AttachCurrency)) {
                return false;
            }
        }

        if (index > 0) {
            var inventory = Player!.Inventories.FindByIndex(index);

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

}