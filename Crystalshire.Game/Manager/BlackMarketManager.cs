using Crystalshire.Database;

using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Mailing;
using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Model.BlackMarket;

using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Repository;
using Crystalshire.Game.Characters;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Manager;

public class BlackMarketManager {
    public IPlayer? Player { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IPlayerRepository? Repository { get; init; }
    public IDatabaseFactory? Factory { get; init; }
    public BlackMarketShop? BlackMarket { get; init; }
    public ContentService? ContentService { get; init; }

    public void SendRequestedItems(BlackMarketItemCategory category, int page) {
        if (BlackMarket is not null) {
            var maximum = BlackMarket.GetPageCount(category);
            var items = BlackMarket.GetItems(category, page);

            PacketSender!.SendBlackMarketItems(Player!, items, maximum);
        }
    }

    public void ProcessPurchaseRequest(int id, int amount, string name) {
        if (BlackMarket is not null) {
            var item = BlackMarket.GetItem(id);

            if (item is not null) {
                if (item.PurchaseLimit > 0) {
                    if (amount > item.PurchaseLimit) {
                        amount = item.PurchaseLimit;
                    }
                }

                if (Player!.Account.Cash < (amount * item.Price)) {
                    PacketSender!.SendAlertMessage(Player!, AlertMessageType.NotEnoughCash, MenuResetType.None);
                }
                else {
                    ContinuePurchaseRequest(item, amount, name);
                }
            }
            else {
                PacketSender!.SendAlertMessage(Player!, AlertMessageType.InvalidItem, MenuResetType.None);
            }
        }
    }

    private async void ContinuePurchaseRequest(BlackMarketItem item, int amount, string name) {
        if (ExistItem(item)) {
            var receiver = GetReceiver(item, name);

            var mailing = CreateMail(item, amount, name);

            var success = true;

            if (receiver is not null) {
                mailing.ReceiverCharacterId = receiver.Character.CharacterId;

                receiver.Mails.Add(mailing);

                PacketSender!.SendMail(receiver, mailing);

                PacketSender!.SendMessage(SystemMessage.YouJustReceivedMail, QbColor.Gold, receiver);
            }
            else {
                var database = new CharacterDatabase(Configuration!, Factory!);

                var id = await database.GetCharacterIdAsync(name);

                if (id != 0) {
                    mailing.ReceiverCharacterId = id;

                    await database.SaveMailAsync(mailing);
                }
                else {
                    success = false;

                    PacketSender!.SendAlertMessage(Player!.GetConnection(), AlertMessageType.InvalidRecipientName, MenuResetType.None);
                }

                database.Dispose();
            }

            if (success) {
                Player!.Account.Cash -= item.Price * amount;

                PacketSender!.SendCash(Player);
                PacketSender!.SendAlertMessage(Player!.GetConnection(), AlertMessageType.SuccessPurchase, MenuResetType.None);
            }
        }
    }

    private CharacterMail CreateMail(BlackMarketItem cashItem, int amount, string receiver) {
        var id = Player!.Character.CharacterId;
        var sender = Configuration!.BlackMarket.Sender;
        var content = Configuration!.BlackMarket.PurchaseMessage;
        var subject = Configuration!.BlackMarket.PurchaseTitle;

        if (string.Compare(Player!.Character.Name, receiver, true) != 0) {
            if (cashItem.CouldSendGift) {
                sender = Player!.Character.Name;

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

    private IPlayer? GetReceiver(BlackMarketItem item, string name) {
        if (!item.CouldSendGift) {
            return Player;
        }

        return Repository!.FindByName(name);
    }
}