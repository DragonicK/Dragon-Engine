using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Shops;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Manager;

public class ShopManager {
    public IPlayer? Player { get; init; }
    public IDatabase<Shop>? Shops { get; init; }
    public IDatabase<Item>? Items { get; init; }
    public IPacketSender? PacketSender { get; init; }

    private const int SingleItemAmount = 1;

    public void ProcessBuyRequest(int index, int amount) {
        var id = Player!.ShopId;
        var shop = GetShop(id);

        if (shop is not null) {
            index--;

            if (index < shop.Items.Count) {
                var item = shop.Items[index];

                ContinueBuyRequest(item, amount);
            }
        }
    }

    private void ContinueBuyRequest(ShopItem shopItem, int amount) {
        var currency = shopItem.CurrencyId;
        var price = shopItem.Price;

        amount = amount <= 0 ? SingleItemAmount : amount;

        var total = Player!.Currencies.Get(currency) - price * amount;

        if (total < 0) {
            PacketSender!.SendMessage(SystemMessage.InsuficientCurrency, QbColor.BrigthRed, Player!);
        }
        else {
            var item = GetItem(shopItem.Id);

            if (item is not null) {
                CharacterInventory? inventory;

                if (item.MaximumStack > 0) {
                    inventory = AddStack(shopItem, amount);
                }
                else {
                    inventory = AddItem(shopItem, SingleItemAmount);
                }

                if (inventory is not null) {
                    var parameters = new string[] {
                            item.Id.ToString(),
                            amount.ToString()
                        };

                    Player!.Currencies.Subtract(currency, price * amount);

                    PacketSender!.SendCurrencyUpdate(Player!, currency);
                    PacketSender!.SendInventoryUpdate(Player!, inventory.InventoryIndex);

                    PacketSender!.SendMessage(SystemMessage.YouObtainedItem, QbColor.BrigthGreen, Player!, parameters);
                }
                else {
                    PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, Player!);
                }
            }
        }
    }

    private CharacterInventory? AddStack(ShopItem item, int amount) {
        var inventory = Player!.Inventories.FindByItemId(item.Id);

        if (inventory is not null) {
            inventory.Value += amount;
        }
        else {
            inventory = AddItem(item, amount);
        }

        return inventory;
    }

    private CharacterInventory? AddItem(ShopItem item, int amount) {
        var maximum = Player!.Character.MaximumInventories;

        var inventory = Player!.Inventories.FindFreeInventory(maximum);

        if (inventory is not null) {
            inventory.ItemId = item.Id;
            inventory.Value = amount;
            inventory.Level = item.Level;
            inventory.Bound = item.Bound;
            inventory.AttributeId = item.AttributeId;
            inventory.UpgradeId = item.UpgradeId;
        }

        return inventory;
    }

    public void ProcessSellRequest(int index, int amount) {
        var shop = GetShop(Player!.ShopId);

        if (shop is not null) {
            var inventory = Player!.Inventories.FindByIndex(index);

            if (inventory is not null) {
                var item = GetItem(inventory.ItemId);

                if (item is not null) {
                    if (item.Price <= 0) {
                        PacketSender!.SendMessage(SystemMessage.ItemCannotBeSold, QbColor.BrigthRed, Player!);
                    }
                    else {
                        ContinueSellRequest(inventory, item, amount);
                    }
                }
            }
        }
    }

    private void ContinueSellRequest(CharacterInventory inventory, Item item, int amount) {
        var index = inventory.InventoryIndex;

        amount = amount <= 0 ? SingleItemAmount : amount;

        var price = item.Price * amount;

        if (Player!.Currencies.Add(CurrencyType.Gold, price)) {
            if (item.MaximumStack > 0) {
                inventory.Value -= amount;

                if (inventory.Value <= 0) {
                    inventory.Clear();
                }
            }
            else {
                amount = 1;

                inventory.Clear();
            }

            PacketSender!.SendInventoryUpdate(Player!, index);
            PacketSender!.SendCurrencyUpdate(Player!, CurrencyType.Gold);

            var parameters = new string[] {
                    item.Id.ToString(),
                    amount.ToString()
                };

            PacketSender!.SendMessage(SystemMessage.ItemHasBeenSold, QbColor.BrigthGreen, Player!, parameters);
        }
        else {
            PacketSender!.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthGreen, Player!);
        }
    }

    private Shop? GetShop(int id) {
        if (Shops is not null) {
            return Shops[id];
        }

        return null;
    }

    private Item? GetItem(int id) {
        if (Items is not null) {
            return Items[id];
        }

        return null;
    }

}