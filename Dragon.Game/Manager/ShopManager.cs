using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Shops;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class ShopManager {
    public ContentService? ContentService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int SingleItemAmount = 1;

    public ShopManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessBuyRequest(IPlayer player, int index, int amount) {
        var shop = GetShop(player.ShopId);
        var sender = GetPacketSender();

        if (shop is not null) {
            index--;

            if (index < shop.Items.Count) {
                var item = shop.Items[index];

                ContinueBuyRequest(sender, player, item, amount);
            }
        }
    }

    private void ContinueBuyRequest(IPacketSender sender, IPlayer player, ShopItem shopItem, int amount) {
        var currency = shopItem.CurrencyId;
        var price = shopItem.Price;

        amount = amount <= 0 ? SingleItemAmount : amount;

        var total = player.Currencies.Get(currency) - price * amount;

        if (total < 0) {
            sender.SendMessage(SystemMessage.InsuficientCurrency, QbColor.BrigthRed, player);
        }
        else {
            var item = GetItem(shopItem.Id);

            if (item is not null) {
                CharacterInventory? inventory;

                if (item.MaximumStack > 0) {
                    inventory = AddStack(player, shopItem, amount);
                }
                else {
                    inventory = AddItem(player, shopItem, SingleItemAmount);
                }

                if (inventory is not null) {
                    var parameters = new string[] { item.Id.ToString(), amount.ToString() };

                    player.Currencies.Subtract(currency, price * amount);

                    sender.SendCurrencyUpdate(player, currency);
                    sender.SendInventoryUpdate(player, inventory.InventoryIndex);

                    sender.SendMessage(SystemMessage.YouObtainedItem, QbColor.BrigthGreen, player, parameters);
                }
                else {
                    sender.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, player);
                }
            }
        }
    }

    private CharacterInventory? AddStack(IPlayer player, ShopItem item, int amount) {
        var inventory = player.Inventories.FindByItemId(item.Id);

        if (inventory is not null) {
            inventory.Value += amount;
        }
        else {
            inventory = AddItem(player, item, amount);
        }

        return inventory;
    }

    private CharacterInventory? AddItem(IPlayer player, ShopItem item, int amount) {
        var maximum = player.Character.MaximumInventories;

        var inventory = player.Inventories.FindFreeInventory(maximum);

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

    public void ProcessSellRequest(IPlayer player, int index, int amount) {
        var shop = GetShop(player.ShopId);
        var sender = GetPacketSender();

        if (shop is not null) {
            var inventory = player.Inventories.FindByIndex(index);

            if (inventory is not null) {
                var item = GetItem(inventory.ItemId);

                if (item is not null) {
                    if (item.Price <= 0) {
                        sender.SendMessage(SystemMessage.ItemCannotBeSold, QbColor.BrigthRed, player);
                    }
                    else {
                        ContinueSellRequest(sender, player, inventory, item, amount);
                    }
                }
            }
        }
    }

    private void ContinueSellRequest(IPacketSender sender, IPlayer player, CharacterInventory inventory, Item item, int amount) {
        var index = inventory.InventoryIndex;

        amount = amount <= 0 ? SingleItemAmount : amount;

        var price = item.Price * amount;

        if (player.Currencies.Add(CurrencyType.Gold, price)) {
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

            sender.SendInventoryUpdate(player, index);
            sender.SendCurrencyUpdate(player, CurrencyType.Gold);

            var parameters = new string[] { item.Id.ToString(), amount.ToString() };

            sender.SendMessage(SystemMessage.ItemHasBeenSold, QbColor.BrigthGreen, player, parameters);
        }
        else {
            sender.SendMessage(SystemMessage.TheCurrencyCannotBeAdded, QbColor.BrigthGreen, player);
        }
    }

    private Shop? GetShop(int id) {
        var shops = GetDatabaseShops();

        shops.TryGet(id, out var shop);

        return shop;
    }

    private Item? GetItem(int id) {
        var items = GetDatabaseItems();

        items.TryGet(id, out var item);

        return item;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Shop> GetDatabaseShops() {
        return ContentService!.Shops;
    }
  
    private IDatabase<Item> GetDatabaseItems() {
        return ContentService!.Items;
    }
}