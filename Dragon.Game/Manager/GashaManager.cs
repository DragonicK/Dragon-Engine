using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Gashas;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Manager;

public class GashaManager {
    public IPlayer? Player { get; init; }
    public IDatabase<Item>? Items { get; init; }
    public IDatabase<Gasha>? Gashas { get; init; }
    public IPacketSender? PacketSender { get; init; }

    public void UseGasha(int index, Item item) {
        var inventory = Player!.Inventories.FindByIndex(index);

        if (Gashas is not null) {
            if (Gashas.Contains(item.GashaBoxId)) {
                var gasha = Gashas[item.GashaBoxId]!;
                var count = gasha.Count;

                if (count > 0) {
                    var maximum = Player!.Character.MaximumInventories;
                    var empty = Player!.Inventories.FindFreeInventory(maximum);

                    if (empty is not null) {
                        if (ContinueUseGasha(empty, gasha)) {
                            inventory!.Value--;

                            if (inventory.Value <= 0) {
                                inventory.Clear();
                            }

                            PacketSender!.SendInventoryUpdate(Player!, inventory.InventoryIndex);
                        }
                    }
                    else {
                        PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, Player!);
                    }
                }
                else {
                    PacketSender!.SendMessage(SystemMessage.ThisBoxIsEmpty, QbColor.BrigthRed, Player!);
                }
            }
        }
    }

    private bool ContinueUseGasha(CharacterInventory inventory, Gasha gasha) {
        if (Items is not null) {
            var maximumTries = 100;
            var tries = 0;

            var count = gasha.Count;

            var random = new Random();
            var success = false;

            GashaItem? reward = null;

            while (!success) {
                var index = random.Next(0, count);
                reward = gasha[index];

                var chance = random.NextDouble();

                if (chance <= reward.Chance) {
                    success = true;
                }

                tries++;

                if (tries >= maximumTries) {
                    break;
                }
            }

            if (success) {
                if (reward is not null) {
                    if (Items.Contains(reward.Id)) {
                        var item = Items[reward.Id]!;

                        if (!CanStackItem(item, reward)) {
                            GiveGashaItem(false, reward, inventory);

                            PacketSender!.SendInventoryUpdate(Player!, inventory.InventoryIndex);

                            var parameters = new string[] {
                                    reward.Id.ToString(),
                                    reward.Value.ToString()
                                };

                            PacketSender!.SendMessage(SystemMessage.YouObtainedItem, QbColor.Yellow, Player!, parameters);
                        }

                        return true;
                    }
                }
            }
        }

        return false;
    }

    private bool CanStackItem(Item item, GashaItem reward) {
        if (item.MaximumStack > 0) {
            var stacked = Player!.Inventories.FindByItemId(item.Id);

            if (stacked is not null) {
                if (stacked.Bound == reward.Bound && stacked.Level == reward.Level) {

                    GiveGashaItem(true, reward, stacked);
                    PacketSender!.SendInventoryUpdate(Player!, stacked.InventoryIndex);

                    var parameters = new string[] {
                            reward.Id.ToString(),
                            reward.Value.ToString()
                        };

                    PacketSender!.SendMessage(SystemMessage.YouObtainedItem, QbColor.Yellow, Player!, parameters);

                    return true;
                }
            }
        }

        return false;
    }

    private void GiveGashaItem(bool isStackable, GashaItem item, CharacterInventory inventory) {
        inventory.ItemId = item.Id;

        if (isStackable) {
            inventory.Value += item.Value;
        }
        else {
            inventory.Value = item.Value;
        }

        inventory.Level = item.Level;
        inventory.Bound = item.Bound;
        inventory.AttributeId = item.AttributeId;
        inventory.UpgradeId = item.UpgradeId;
        inventory.Charge = item.Charge;
        inventory.IsPacked = item.IsPacked;
        inventory.WrappableCount = item.WrappableCount;
        inventory.FusionedItemId = item.FusionedItemId;
        inventory.Socket = item.Socket;
        inventory.FusionedSocket = item.FusionedSocket;
        inventory.ActivationCount = item.ActivationCount;
        inventory.ItemSkinId = item.ItemSkinId;
    }
}