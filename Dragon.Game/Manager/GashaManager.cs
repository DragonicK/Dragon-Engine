using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Gashas;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class GashaManager {
    public ContentService? ContentService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumTries = 100;

    public GashaManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void UseGasha(IPlayer player, int index, Item item) {
        var sender = GetPacketSender();
        var gashas = GetDatabaseGasha();

        var inventory = player.Inventories.FindByIndex(index);

        gashas.TryGet(item.GashaBoxId, out var gasha);

        if (gasha is not null) {
            var count = gasha.Count;

            if (count > 0) {
                var maximum = player.Character.MaximumInventories;
                var empty = player.Inventories.FindFreeInventory(maximum);

                if (empty is not null) {
                    if (ContinueUseGasha(sender, player, empty, gasha)) {
                        inventory!.Value--;

                        if (inventory.Value <= 0) {
                            inventory.Clear();
                        }

                        sender.SendInventoryUpdate(player, inventory.InventoryIndex);
                    }
                }
                else {
                    sender.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, player);
                }
            }
            else {
                sender.SendMessage(SystemMessage.ThisBoxIsEmpty, QbColor.BrigthRed, player);
            }
        }
    }

    private bool ContinueUseGasha(IPacketSender sender, IPlayer player, CharacterInventory inventory, Gasha gasha) {
        var items = GetDatabaseItem();

        var reward = GetRandomReward(gasha);

        if (reward is not null) {
            items.TryGet(reward.Id, out var item);

            if (item is not null) {
                if (!CanStackItem(sender, player, item, reward)) {

                    GiveGashaItemToInventory(false, reward, inventory);

                    sender.SendInventoryUpdate(player, inventory.InventoryIndex);

                    var parameters = new string[] { reward.Id.ToString(), reward.Value.ToString() };

                    sender.SendMessage(SystemMessage.YouObtainedItem, QbColor.Yellow, player, parameters);
                }

                return true;
            }
        }
        return false;
    }

    private GashaItem? GetRandomReward(Gasha gasha) {
        var tries = 0;
        var success = false;
        var count = gasha.Count;
        var random = new Random();

        GashaItem? reward = default;

        while (!success) {
            var index = random.Next(0, count);

            reward = gasha[index];

            var chance = random.NextDouble();

            if (chance <= reward.Chance) {
                success = true;
            }

            tries++;

            if (tries >= MaximumTries) {
                break;
            }
        }

        return reward;
    }

    private bool CanStackItem(IPacketSender sender, IPlayer player, Item item, GashaItem reward) {
        if (item.MaximumStack > 0) {
            var stacked = player.Inventories.FindByItemId(item.Id);

            if (stacked is not null) {
                if (stacked.Bound == reward.Bound && stacked.Level == reward.Level) {

                    GiveGashaItemToInventory(true, reward, stacked);

                    sender.SendInventoryUpdate(player, stacked.InventoryIndex);

                    var parameters = new string[] { reward.Id.ToString(), reward.Value.ToString() };

                    sender.SendMessage(SystemMessage.YouObtainedItem, QbColor.Yellow, player, parameters);

                    return true;
                }
            }
        }

        return false;
    }

    private void GiveGashaItemToInventory(bool isStackable, GashaItem item, CharacterInventory inventory) {
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

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Item> GetDatabaseItem() {
        return ContentService!.Items;
    }

    private IDatabase<Gasha> GetDatabaseGasha() {
        return ContentService!.Gashas;
    }
}