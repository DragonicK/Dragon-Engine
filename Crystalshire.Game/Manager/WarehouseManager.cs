using Crystalshire.Core.Model;
using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;

using Crystalshire.Game.Network;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Manager {
    public class WarehouseManager {
        public IPlayer? Player { get; init; }
        public IDatabase<Item>? Items { get; init; }
        public IPacketSender? PacketSender { get; init; }

        private const int Invalid = -1;

        public void Deposit(int fromInventoryIndex, int amount) {
            if (Items is null) {
                return;
            }

            var inventory = Player!.Inventories.FindByIndex(fromInventoryIndex);

            if (inventory is not null) {
                var value = inventory.Value;

                if (amount > value) {
                    amount = value;
                }

                if (Items.Contains(inventory.ItemId)) {
                    var maximum = Player!.Character.MaximumWarehouse;
                    var item = Items![inventory.ItemId];
                    int index;

                    if (item!.MaximumStack > 0) {
                        index = Player!.Warehouse.Deposit(inventory, amount, maximum);
                    }
                    else {
                        index = Player!.Warehouse.Deposit(inventory, maximum);
                    }

                    if (index != Invalid) {
                        PacketSender!.SendInventoryUpdate(Player, fromInventoryIndex);
                        PacketSender!.SendWarehouseUpdate(Player, index);
                    }
                    else {
                        PacketSender!.SendMessage(SystemMessage.WarehouseFull, QbColor.Red, Player!);
                    }
                }
            }
        }

        public void Withdraw(int fromWarehouseIndex, int amount) {
            if (Items is null) {
                return;
            }

            var warehouse = Player!.Warehouse.FindByIndex(fromWarehouseIndex);

            if (warehouse is not null) {
                var value = warehouse.Value;

                if (amount > value) {
                    amount = value;
                }

                if (Items.Contains(warehouse.ItemId)) {
                    var maximum = Player!.Character.MaximumInventories;
                    var item = Items[warehouse.ItemId];
                    int index;

                    if (item!.MaximumStack > 0) {
                        index = Player!.Inventories.Add(warehouse, amount, maximum);
                    }
                    else {
                        index = Player!.Inventories.Add(warehouse, maximum);
                    }

                    if (index != Invalid) {
                        PacketSender!.SendInventoryUpdate(Player, index);
                        PacketSender!.SendWarehouseUpdate(Player, fromWarehouseIndex);
                    }
                    else {
                        PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.Red, Player!);
                    }
                }
            }
        }
    }
}