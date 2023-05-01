using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.BlackMarket;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Repository;
using Dragon.Game.Characters;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class BlackMarketManager {
    public ContentService? ContentService { get; private set; }
    public DatabaseService? DatabaseService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public BlackMarketService? BlackMarketService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly ICharacterDatabase CharacterDatabase;

    public BlackMarketManager(IServiceInjector injector) {
        injector.Inject(this);

        CharacterDatabase = new CharacterDatabase(Configuration!, DatabaseService!.DatabaseFactory!);
    }

    public void SendRequestedItems(IPlayer player, BlackMarketItemCategory category, int page) {
        var sender = GetPacketSender();
        var market = GetBlackMarketShop();

        var maximum = market.GetPageCount(category);
        var items = market.GetItems(category, page);

        sender.SendBlackMarketItems(player, items, maximum);
    }

    public void ProcessPurchaseRequest(IPlayer player, int id, int amount, string name) {
        var sender = GetPacketSender();
        var market = GetBlackMarketShop();

        var item = market.GetItem(id);

        if (item is not null) {
            if (item.PurchaseLimit > 0) {
                if (amount > item.PurchaseLimit) {
                    amount = item.PurchaseLimit;
                }
            }

            if (player.Account.Cash < (amount * item.Price)) {
                sender.SendAlertMessage(player, AlertMessageType.NotEnoughCash, MenuResetType.None);
            }
            else {
                ContinuePurchaseRequest(sender, player, item, amount, name);
            }
        }
        else {
            sender.SendAlertMessage(player, AlertMessageType.InvalidItem, MenuResetType.None);
        }
    }

    private async void ContinuePurchaseRequest(IPacketSender sender, IPlayer player, BlackMarketItem item, int amount, string name) {
        if (ExistItem(item)) {
            var receiver = GetReceiver(player, item, name);

            var mailing = CreateMail(player, item, amount, name);

            var success = true;

            if (receiver is not null) {
                mailing.ReceiverCharacterId = receiver.Character.CharacterId;

                receiver.Mails.Add(mailing);

                sender.SendMail(receiver, mailing);

                sender.SendMessage(SystemMessage.YouJustReceivedMail, QbColor.Gold, receiver);
            }
            else {
                var id = await CharacterDatabase.GetCharacterIdAsync(name);

                if (id != 0) {
                    mailing.ReceiverCharacterId = id;

                    await CharacterDatabase.SaveMailAsync(mailing);
                }
                else {
                    success = false;

                    sender.SendAlertMessage(player.GetConnection(), AlertMessageType.InvalidRecipientName, MenuResetType.None);
                }
            }

            if (success) {
                player.Account.Cash -= item.Price * amount;

                sender.SendCash(player);
                sender.SendAlertMessage(player.GetConnection(), AlertMessageType.SuccessPurchase, MenuResetType.None);
            }
        }
    }

    private CharacterMail CreateMail(IPlayer player,  BlackMarketItem cashItem, int amount, string receiver) {
        var id = player.Character.CharacterId;
        var sender = Configuration!.BlackMarket.Sender;
        var content = Configuration!.BlackMarket.PurchaseMessage;
        var subject = Configuration!.BlackMarket.PurchaseTitle;

        if (string.Compare(player.Character.Name, receiver, true) != 0) {
            if (cashItem.CouldSendGift) {
                sender = player.Character.Name;

                content = Configuration.BlackMarket.GiftMessage.Replace("{NAME}", sender);
                subject = Configuration.BlackMarket.GiftTitle;
            }
        }

        var mailing = new CharacterMail() {
            AttachItemFlag = true,
            Subject = subject,
            Content = content,
            SendDate = DateTime.Now,
            SenderCharacterId = id,
            SenderCharacterName = sender,
            MailAttachItem = new CharacterMailAttachItem() {
                ItemId = cashItem.ItemId,
                Value = amount,
                Level = cashItem.Level,
                Bound = cashItem.Bound,
                AttributeId = cashItem.AttributeId,
                UpgradeId = cashItem.UpgradeId
            }
        };

        mailing.ExpireDate = mailing.SendDate.AddDays(Configuration!.Mail.TimeLimitInDays);

        return mailing;
    }

    private bool ExistItem(BlackMarketItem item) {
        var items = ContentService!.Items;

        if (items is not null) {
            return items.Contains(item.ItemId);
        }

        return false;
    }

    private IPlayer? GetReceiver(IPlayer player, BlackMarketItem item, string name) {
        if (!item.CouldSendGift) {
            return player;
        }

        return GetPlayerRepository().FindByName(name);
    }

    private BlackMarketShop GetBlackMarketShop() {
        return BlackMarketService!.BlackMarket!;
    }

    private IPlayerRepository GetPlayerRepository() {
        return ConnectionService!.PlayerRepository!;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}