using Dragon.Core.Model;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Services;

namespace Dragon.Game.Manager;

public class ReceiveFromMailManager {
    public IPlayer? Player { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public ContentService? ContentService { get; init; }

    public void ReceiveCurrency(int id) {
        var mail = Player!.Mails.Get(id);

        if (mail is not null) {
            if (!mail.AttachCurrencyReceiveFlag) {
                if (Player!.Currencies.Add(CurrencyType.Gold, mail.AttachCurrency)) {
                    mail.AttachCurrencyReceiveFlag = true;

                    PacketSender!.SendCurrencyUpdate(Player, CurrencyType.Gold);
                    PacketSender!.SendMailUpdate(Player!, id, mail.AttachCurrencyReceiveFlag, mail.AttachItemReceiveFlag);
                }
            }
        }
    }

    public void ReceiveItem(int id) {
        var mail = Player!.Mails.Get(id);

        if (mail is not null) {
            if (!mail.AttachItemReceiveFlag) {
                var attachedItem = mail.MailAttachItem!;
                var item = GetItem(attachedItem.ItemId);

                if (item is not null) {
                    var inventory = GetInventory(attachedItem.ItemId, out var isStacked);

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

                        PacketSender!.SendInventoryUpdate(Player!, inventory.InventoryIndex);
                        PacketSender!.SendMailUpdate(Player!, id, mail.AttachCurrencyReceiveFlag, mail.AttachItemReceiveFlag);
                    }
                    else {
                        PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, Player!);
                    }
                }
            }
        }
    }

    private Item? GetItem(int id) {
        var items = ContentService!.Items;

        if (items is not null) {
            if (items.Contains(id)) {
                return items[id];
            }
        }

        return null;
    }

    private CharacterInventory? GetInventory(int id, out bool isStacked) {
        isStacked = false;

        var items = ContentService!.Items;

        CharacterInventory? inventory = default;

        if (items.Contains(id)) {
            var item = items[id]!;

            if (item.MaximumStack > 0) {
                isStacked = true;

                inventory = Player!.Inventories.FindByItemId(item.Id);
            }

            if (inventory is null) {
                isStacked = false;

                var maximum = Player!.Character.MaximumInventories;

                inventory = Player!.Inventories.FindFreeInventory(maximum);
            }
        }

        return inventory;
    }

}