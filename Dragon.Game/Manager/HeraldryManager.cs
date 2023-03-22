using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Heraldries;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Instances;
using Dragon.Game.Services;
using Dragon.Game.Configurations;

namespace Dragon.Game.Manager;

public class HeraldryManager {
    public IPlayer? Player { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IDatabase<Item>? Items { get; init; }
    public IDatabase<Heraldry>? Heraldries { get; init; }
    public InstanceService? InstanceService { get; init; }

    private const int MaximumAttempts = 10;

    public void UnequipHeraldry(int index) {
        var heraldry = Player!.Heraldries.Get(index);

        if (heraldry is not null) {
            if (heraldry.ItemId > 0) {
                var maximum = Player!.Character.MaximumInventories;
                var inventory = Player!.Inventories.FindFreeInventory(maximum);

                if (inventory is not null) {
                    inventory.Apply(heraldry);
                    Player!.Heraldries.Unequip(index);

                    PacketSender!.SendInventoryUpdate(Player, inventory.InventoryIndex);
                    PacketSender!.SendHeraldryUpdate(Player, index);

                    SendAttributes();
                }
                else {
                    PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.Red, Player);
                }
            }
        }
    }

    public void EquipHeraldry(int index, Item item) {
        if (item.EquipmentId > 0) {
            var inventory = Player!.Inventories.FindByIndex(index)!;

            if (inventory.AttributeId == 0) {
                if (CanRevealHeraldryAttribute(inventory, item)) {
                    PacketSender!.SendInventoryUpdate(Player, index);
                }
            }
            else {
                var maximum = Configuration!.Player.MaximumHeraldries;
                var heraldry = Player!.Heraldries.FindFreeHeraldry(maximum);

                if (heraldry is not null) {
                    ContinueEquipHeraldryAtIndex(heraldry.InventoryIndex, index);
                }
            }
        }
    }

    public void EquipHeraldryAtIndewx(int heraldryIndex, int index) {
        var item = GetItemFromInventory(index);

        if (item is not null) {

            if (item.ClassCode != Player!.Character.ClassCode) {
                var parameters = new string[] { item.ClassCode.ToString() };

                PacketSender!.SendMessage(SystemMessage.OnlyClassCodeCanUseItem, QbColor.Red, Player, parameters);
            }
            else {
                if (item!.RequiredLevel > Player!.Character.Level) {
                    PacketSender!.SendMessage(SystemMessage.YouDoNotHaveRequiredLevel, QbColor.Red, Player);
                }
                else {
                    if (item.Type == ItemType.Heraldry) {
                        ContinueEquipHeraldryAtIndex(heraldryIndex, index);
                    }
                }
            }
        }
    }

    private void ContinueEquipHeraldryAtIndex(int heraldryIndex, int index) {
        var inventory = Player!.Inventories.FindByIndex(index);

        if (inventory!.AttributeId > 0) {
            if (Player!.Heraldries.IsEquipped(heraldryIndex)) {
                SwapHeraldry(heraldryIndex, inventory);
            }
            else {
                Player!.Heraldries.Equip(heraldryIndex, inventory);
                inventory.Clear();
            }

            PacketSender!.SendHeraldryUpdate(Player, heraldryIndex);
            PacketSender!.SendInventoryUpdate(Player, index);

            SendAttributes();
        }
        else {
            PacketSender!.SendMessage(SystemMessage.NeedRevealItemAttribute, QbColor.BrigthRed, Player);
        }
    }

    private bool CanRevealHeraldryAttribute(CharacterInventory inventory, Item item) {
        var heraldryId = item.EquipmentId;
        var success = false;

        if (Heraldries!.Contains(heraldryId)) {
            var heraldry = Heraldries[heraldryId];
            var random = new Random();
            var attempts = 0;
            var count = 0;

            if (heraldry!.Attributes.Count > 0) {
                while (!success) {
                    var chance = random.Next(1, 100);

                    if (chance <= heraldry.Attributes[count].Chance) {
                        inventory.AttributeId = heraldry.Attributes[count].AttributeId;
                        inventory.UpgradeId = heraldry.UpgradeId;
                        success = true;
                    }

                    count++;

                    if (count == heraldry.Attributes.Count) {
                        count = 0;
                    }

                    attempts++;

                    if (attempts > MaximumAttempts) {
                        break;
                    }
                }

                if (success) {
                    PacketSender!.SendMessage(SystemMessage.SuccessToRevealItemAttribute, QbColor.Red, Player!);
                }
                else {
                    PacketSender!.SendMessage(SystemMessage.FailedToRevealItemAttribute, QbColor.Red, Player!);
                }
            }
            else {
                PacketSender!.SendMessage(SystemMessage.CannotRevealItemAttribute, QbColor.Red, Player!);
            }
        }

        return success;
    }

    private void SwapHeraldry(int index, CharacterInventory inventory) {
        var heraldry = Player!.Heraldries.Get(index);

        if (heraldry is not null) {
            var clone = heraldry!.Clone();

            Player!.Heraldries.Unequip(index);
            Player!.Heraldries.Equip(index, inventory);

            inventory.Clear();
            inventory.Apply(clone);
        }
    }

    private Item? GetItemFromInventory(int index) {
        if (Items is not null) {
            var inventory = Player!.Inventories.FindByIndex(index);

            if (inventory is not null) {
                var itemId = inventory.ItemId;

                if (Items.Contains(itemId)) {
                    return Items[itemId];
                }
            }
        }

        return null;
    }

    private void SendAttributes() {
        Player!.AllocateAttributes();
        PacketSender!.SendAttributes(Player);

        var instance = GetInstance();

        if (instance is not null) {
            PacketSender!.SendPlayerVital(Player, instance);
        }
        else {
            PacketSender!.SendPlayerVital(Player);
        }
    }

    private IInstance? GetInstance() {
        var instanceId = Player!.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }

}