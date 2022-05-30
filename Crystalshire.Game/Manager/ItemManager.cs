using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Equipments;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Configurations;
using Crystalshire.Game.Players;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Manager;

public class ItemManager {
    public IPlayer? Player { get; init; }
    public IConfiguration? Configuration { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }
    public ContentService? ContentService { get; init; }

    private const int MaximumAttempts = 10;

    #region Use Item

    public void UseItem(int index) {
        var item = GetItemFromInventory(index);

        if (item is not null) {
            if (item.ClassCode > 0 && item.ClassCode != Player!.Character.ClassCode) {
                var parameters = new string[] { item.ClassCode.ToString() };

                PacketSender!.SendMessage(SystemMessage.OnlyClassCodeCanUseItem, QbColor.Red, Player, parameters);

                return;
            }

            if (item.RequiredLevel > Player!.Character.Level) {
                PacketSender!.SendMessage(SystemMessage.YouDoNotHaveRequiredLevel, QbColor.Red, Player);
            }
            else {
                switch (item!.Type) {
                    case ItemType.Potion:
                        var consume = new ConsumableManager() {
                            InstanceService = InstanceService,
                            Effects = ContentService!.Effects,
                            PacketSender = PacketSender,
                            Player = Player
                        };

                        consume.UsePotion(index, item);

                        break;

                    case ItemType.Equipment:
                        EquipItem(index, item);
                        break;

                    case ItemType.Heraldry:
                        var heraldry = new HeraldryManager() {
                            Heraldries = ContentService!.Heraldries,
                            InstanceService = InstanceService,
                            Items = ContentService!.Items,
                            PacketSender = PacketSender,
                            Configuration = Configuration,
                            Player = Player
                        };

                        heraldry.EquipHeraldry(index, item);

                        break;

                    case ItemType.Recipe:
                        var recipe = new RecipeManager() {
                            Recipes = ContentService!.Recipes,
                            Items = ContentService!.Items,
                            Configuration = Configuration,
                            PacketSender = PacketSender,
                            Player = Player
                        };

                        recipe.UseRecipe(index, item);

                        break;

                    case ItemType.GashaBox:
                        var gasha = new GashaManager() {
                            Gashas = ContentService!.Gashas,
                            Items = ContentService!.Items,
                            PacketSender = PacketSender,
                            Player = Player
                        };

                        gasha.UseGasha(index, item);

                        break;

                    case ItemType.Food:
                        var food = new ConsumableManager() {
                            Effects = ContentService!.Effects,
                            InstanceService = InstanceService,
                            PacketSender = PacketSender,
                            Player = Player
                        };

                        food.UseConsumable(index, item);

                        break;

                    case ItemType.Skill:
                        var skill = new SkillManager() {
                            Passives = ContentService!.Passives,
                            InstanceService = InstanceService,
                            Skills = ContentService!.Skills,
                            PacketSender = PacketSender,
                            Player = Player
                        };

                        skill.LearnFromInventory(index, item);

                        break;

                    case ItemType.Scroll:
                        var scroll = new ConsumableManager() {
                            Effects = ContentService!.Effects,
                            InstanceService = InstanceService,
                            PacketSender = PacketSender,
                            Player = Player
                        };

                        scroll.UseConsumable(index, item);

                        break;
                }
            }
        }
    }

    #endregion

    public void DestroyItem(int index) {
        var inventory = Player!.Inventories.FindByIndex(index);

        if (inventory is not null) {
            inventory.Clear();

            PacketSender!.SendInventoryUpdate(Player, index);
        }
    }

    public void UnequipItem(PlayerEquipmentType index) {
        var equipment = Player!.Equipments.Get(index);

        if (equipment is not null) {
            if (equipment.ItemId > 0) {
                var maximum = Player.Character.MaximumInventories;
                var inventory = Player!.Inventories.FindFreeInventory(maximum);

                if (inventory is not null) {
                    if (index == PlayerEquipmentType.Costume) {
                        Player.Character.CostumeModel = 0;

                        var instance = GetInstance();

                        if (instance is not null) {
                            PacketSender!.SendPlayerModel(Player, instance);
                        }
                    }

                    inventory.Apply(equipment);
                    Player!.Equipments.Unequip(index);

                    PacketSender!.SendInventoryUpdate(Player, inventory.InventoryIndex);
                    PacketSender!.SendEquipmentUpdate(Player, index);

                    SendAttributes();
                }
                else {
                    PacketSender!.SendMessage(SystemMessage.InventoryFull, QbColor.Red, Player);
                }
            }
        }
    }

    private Item? GetItemFromInventory(int index) {
        var items = ContentService!.Items;

        if (items is not null) {
            var inventory = Player!.Inventories.FindByIndex(index);

            if (inventory is not null) {
                var itemId = inventory.ItemId;

                if (items.Contains(itemId)) {
                    return items[itemId];
                }
            }
        }

        return null;
    }

    private void EquipItem(int index, Item item) {
        if (item.EquipmentId > 0) {
            var inventory = Player!.Inventories.FindByIndex(index)!;

            if (inventory.AttributeId == 0) {
                if (CanRevealItemAttribute(inventory, item)) {
                    PacketSender!.SendInventoryUpdate(Player, index);
                }
            }
            else {
                var equipment = GetEquipmentFromItem(item);

                if (equipment is not null) {
                    var type = GetPlayerEquipmentType(equipment.Type);

                    if (type == PlayerEquipmentType.Costume) {
                        ChangeAndSendModel(equipment);
                    }

                    if (Player.Equipments.IsEquipped(type)) {
                        SwapEquipment(type, inventory);
                    }
                    else {
                        Player!.Equipments.Equip(type, inventory);
                        inventory.Clear();
                    }

                    PacketSender!.SendInventoryUpdate(Player, index);
                    PacketSender!.SendEquipmentUpdate(Player, type);

                    SendAttributes();
                }
            }
        }
    }

    private bool CanRevealItemAttribute(CharacterInventory inventory, Item item) {
        var equipments = ContentService!.Equipments;
        var equipmentId = item.EquipmentId;
        var success = false;

        if (equipments!.Contains(equipmentId)) {
            var equipment = equipments[equipmentId];
            var random = new Random();
            var attempts = 0;
            var count = 0;

            if (equipment!.Attributes.Count > 0) {
                while (!success) {
                    var chance = random.Next(1, 100);

                    if (chance <= equipment.Attributes[count].Chance) {
                        inventory.AttributeId = equipment.Attributes[count].AttributeId;
                        inventory.UpgradeId = equipment.UpgradeId;
                        success = true;
                    }

                    count++;

                    if (count == equipment.Attributes.Count) {
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

    private Equipment? GetEquipmentFromItem(Item item) {
        var equipments = ContentService!.Equipments;

        if (item.EquipmentId > 0) {
            if (equipments!.Contains(item.EquipmentId)) {
                return equipments[item.EquipmentId];
            }
        }

        return null;
    }

    private PlayerEquipmentType GetPlayerEquipmentType(EquipmentType type) => type switch {
        EquipmentType.Weapon => PlayerEquipmentType.Weapon,
        EquipmentType.Shield => PlayerEquipmentType.Shield,
        EquipmentType.Helmet => PlayerEquipmentType.Helmet,
        EquipmentType.Armor => PlayerEquipmentType.Armor,
        EquipmentType.Shoulder => PlayerEquipmentType.Shoulder,
        EquipmentType.Belt => PlayerEquipmentType.Belt,
        EquipmentType.Gloves => PlayerEquipmentType.Gloves,
        EquipmentType.Pants => PlayerEquipmentType.Pants,
        EquipmentType.Boots => PlayerEquipmentType.Boots,
        EquipmentType.Necklace => PlayerEquipmentType.Necklace,
        EquipmentType.Earring => GetEquipmentFreeIndex(EquipmentType.Earring),
        EquipmentType.Ring => GetEquipmentFreeIndex(EquipmentType.Ring),
        EquipmentType.Bracelet => GetEquipmentFreeIndex(EquipmentType.Bracelet),
        EquipmentType.Costume => PlayerEquipmentType.Costume,

        _ => PlayerEquipmentType.None
    };

    private PlayerEquipmentType GetEquipmentFreeIndex(EquipmentType type) {
        var index = PlayerEquipmentType.None;

        // Check if there're empty slots.
        if (type == EquipmentType.Earring) {
            if (Player!.Equipments.IsEquipped(PlayerEquipmentType.Earring_1) == false) {
                index = PlayerEquipmentType.Earring_1;
            }
            else if (Player!.Equipments.IsEquipped(PlayerEquipmentType.Earring_2) == false) {
                index = PlayerEquipmentType.Earring_2;
            }

            // Case else, swap the 1st slot.
            if (index == PlayerEquipmentType.None) {
                index = PlayerEquipmentType.Earring_1;
            }

            return index;
        }

        if (type == EquipmentType.Earring) {
            if (Player!.Equipments.IsEquipped(PlayerEquipmentType.Ring_1) == false) {
                index = PlayerEquipmentType.Ring_1;
            }
            else if (Player!.Equipments.IsEquipped(PlayerEquipmentType.Ring_2) == false) {
                index = PlayerEquipmentType.Ring_2;
            }

            if (index == PlayerEquipmentType.None) {
                index = PlayerEquipmentType.Ring_1;
            }

            return index;
        }

        if (type == EquipmentType.Bracelet) {
            if (Player!.Equipments.IsEquipped(PlayerEquipmentType.Bracelet_1) == false) {
                index = PlayerEquipmentType.Bracelet_1;
            }
            else if (Player!.Equipments.IsEquipped(PlayerEquipmentType.Bracelet_2) == false) {
                index = PlayerEquipmentType.Bracelet_2;
            }

            if (index == PlayerEquipmentType.None) {
                index = PlayerEquipmentType.Bracelet_1;
            }

            return index;
        }

        return index;
    }

    private void SwapEquipment(PlayerEquipmentType index, CharacterInventory inventory) {
        var equipment = Player!.Equipments.Get(index);

        if (equipment is not null) {
            var clone = equipment!.Clone();

            Player!.Equipments.Unequip(index);
            Player!.Equipments.Equip(index, inventory);

            inventory.Clear();
            inventory.Apply(clone);
        }
    }

    private void ChangeAndSendModel(Equipment equipment) {
        Player!.Character.CostumeModel = equipment.ModelId;

        var instance = GetInstance();

        if (instance is not null) {
            PacketSender!.SendPlayerModel(Player, instance);
        }
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