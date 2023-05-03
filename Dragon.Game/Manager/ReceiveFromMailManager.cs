using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class ReceiveFromMailManager {
    public ContentService? ContentService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public ReceiveFromMailManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ReceiveCurrency(IPlayer player, int id) {
        var sender = GetPacketSender();
        var mail = player.Mails.Get(id);

        if (mail is not null) {
            if (!mail.AttachCurrencyReceiveFlag) {
                if (player.Currencies.Add(CurrencyType.Gold, mail.AttachCurrency)) {
                    mail.AttachCurrencyReceiveFlag = true;

                    sender.SendCurrencyUpdate(player, CurrencyType.Gold);
                    sender.SendMailUpdate(player, id, mail.AttachCurrencyReceiveFlag, mail.AttachItemReceiveFlag);
                }
            }
        }
    }

    public void ReceiveItem(IPlayer player, int id) {
        var sender = GetPacketSender();
        var mail = player.Mails.Get(id);

        if (mail is not null) {
            if (!mail.AttachItemReceiveFlag) {
                var attachedItem = mail.MailAttachItem!;
                var item = GetItem(attachedItem.ItemId);

                if (item is not null) {
                    var inventory = GetInventory(player, attachedItem.ItemId, out var isStacked);

                    if (inventory is not null) {
                        if (isStacked) {
                            inventory.Value += attachedItem.Value;
                        }
                        else {
                            inventory.ItemId = attachedItem.ItemId;
                            inventory.Value = attachedItem.Value;
                            inventory.Level = attachedItem.Level;
                            inventory.AttributeId = attachedItem.AttributeId;
                            inventory.UpgradeId = attachedItem.UpgradeId;
                        }

                        mail.AttachItemReceiveFlag = true;

                        sender.SendInventoryUpdate(player, inventory.InventoryIndex);
                        sender.SendMailUpdate(player, id, mail.AttachCurrencyReceiveFlag, mail.AttachItemReceiveFlag);
                    }
                    else {
                        sender.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, player);
                    }
                }
            }
        }
    }

    private Item? GetItem(int id) {
        var items = ContentService!.Items!;

        items.TryGet(id, out var item);

        return item;
    }

    private CharacterInventory? GetInventory(IPlayer player, int id, out bool isStacked) {
        isStacked = false;

        var items = ContentService!.Items!;

        CharacterInventory? inventory = default;

        items.TryGet(id, out var item);

        if (item is not null) {
            if (item.MaximumStack > 0) {
                isStacked = true;

                inventory = player.Inventories.FindByItemId(item.Id);
            }

            if (inventory is null) {
                isStacked = false;

                var maximum = player.Character.MaximumInventories;

                inventory = player.Inventories.FindFreeInventory(maximum);
            }
        }

        return inventory;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}