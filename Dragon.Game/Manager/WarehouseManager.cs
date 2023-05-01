using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class WarehouseManager {
    public ContentService? ContentService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    private const int Invalid = -1;

    public WarehouseManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Deposit(IPlayer player, int fromInventoryIndex, int amount) {
        var items = GetDatabaseItem();
        var sender = GetPacketSender();

        var inventory = player.Inventories.FindByIndex(fromInventoryIndex);

        if (inventory is not null) {
            var value = inventory.Value;

            if (amount > value) {
                amount = value;
            }

            items.TryGet(inventory.ItemId, out var item);

            if (item is not null) {
                int index;

                var maximum = player.Character.MaximumWarehouse;

                if (item!.MaximumStack > 0) {
                    index = player.Warehouse.Deposit(inventory, amount, maximum);
                }
                else {
                    index = player.Warehouse.Deposit(inventory, maximum);
                }

                if (index != Invalid) {
                    sender.SendInventoryUpdate(player, fromInventoryIndex);
                    sender.SendWarehouseUpdate(player, index);
                }
                else {
                    sender.SendMessage(SystemMessage.WarehouseFull, QbColor.Red, player);
                }
            }
        }
    }

    public void Withdraw(IPlayer player, int fromWarehouseIndex, int amount) {
        var items = GetDatabaseItem();
        var sender = GetPacketSender();

        var warehouse = player.Warehouse.FindByIndex(fromWarehouseIndex);

        if (warehouse is not null) {
            var value = warehouse.Value;

            if (amount > value) {
                amount = value;
            }

            items.TryGet(warehouse.ItemId, out var item);

            if (item is not null) {
                int index;

                var maximum = player.Character.MaximumInventories;

                if (item!.MaximumStack > 0) {
                    index = player.Inventories.Add(warehouse, amount, maximum);
                }
                else {
                    index = player.Inventories.Add(warehouse, maximum);
                }

                if (index != Invalid) {
                    sender.SendInventoryUpdate(player, index);
                    sender.SendWarehouseUpdate(player, fromWarehouseIndex);
                }
                else {
                    sender.SendMessage(SystemMessage.InventoryFull, QbColor.Red, player);
                }
            }
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Item> GetDatabaseItem() {
        return ContentService!.Items;
    }
}