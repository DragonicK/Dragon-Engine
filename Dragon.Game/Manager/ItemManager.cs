using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Equipments;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class ItemManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumAttempts = 10;

    private readonly GashaManager GashaManager;
    private readonly SkillManager SkillManager;
    private readonly RecipeManager RecipeManager;
    private readonly HeraldryManager HeraldryManager;
    private readonly ConsumableManager ConsumableManager;

    public ItemManager(IServiceInjector injector) {
        injector.Inject(this);

        GashaManager = new GashaManager(injector);
        SkillManager = new SkillManager(injector);
        RecipeManager = new RecipeManager(injector);
        HeraldryManager = new HeraldryManager(injector);
        ConsumableManager = new ConsumableManager(injector);
    }

    #region Use Item

    public void UseItem(IPlayer player, int index) {
        var sender = GetPacketSender();
        var item = GetItemFromInventory(player, index);

        if (item is not null) {
            if (item.ClassCode > 0 && item.ClassCode != player.Character.ClassCode) {
                var parameters = new string[] { item.ClassCode.ToString() };

                sender.SendMessage(SystemMessage.OnlyClassCodeCanUseItem, QbColor.Red, player, parameters);

                return;
            }

            if (item.RequiredLevel > player.Character.Level) {
                sender.SendMessage(SystemMessage.YouDoNotHaveRequiredLevel, QbColor.Red, player);
            }
            else {
                switch (item.Type) {
                    case ItemType.Potion:
                        ConsumableManager.UsePotion(player, index, item);
                        break;

                    case ItemType.Equipment:
                        EquipItem(sender, player, index, item);
                        break;

                    case ItemType.Heraldry:
                        HeraldryManager.EquipHeraldry(player, index, item);
                        break;

                    case ItemType.Recipe:
                        RecipeManager.UseRecipe(player, index, item);
                        break;

                    case ItemType.GashaBox:
                        GashaManager.UseGasha(player, index, item);
                        break;

                    case ItemType.Food:
                        ConsumableManager.UseConsumable(player, index, item);
                        break;

                    case ItemType.Skill:
                        SkillManager.LearnFromInventory(player, index, item);
                        break;

                    case ItemType.Scroll:
                        ConsumableManager.UseConsumable(player, index, item);
                        break;
                }
            }
        }
    }

    #endregion

    #region Equip Item

    private void EquipItem(IPacketSender sender, IPlayer player, int index, Item item) {
        if (item.EquipmentId > 0) {
            var inventory = player.Inventories.FindByIndex(index)!;

            if (inventory.AttributeId == 0) {
                if (CanRevealItemAttribute(sender, player, inventory, item)) {
                    sender.SendInventoryUpdate(player, index);
                }
            }
            else {
                var equipment = GetEquipmentFromItem(item);

                if (equipment is not null) {
                    var type = GetPlayerEquipmentType(player, equipment.Type);

                    var isNeededTwoHandedStyleSwap = false;
                    var successfullySwapped = true;

                    if (type == PlayerEquipmentType.Costume) {
                        ChangeAndSendModel(sender, player, equipment);
                    }

                    if (equipment.Type == EquipmentType.Weapon || equipment.Type == EquipmentType.Shield) {
                        isNeededTwoHandedStyleSwap = IsNeededTwoHandedStyleSwap(player, equipment);
                    }

                    if (!isNeededTwoHandedStyleSwap) {
                        if (player.Equipments.IsEquipped(type)) {
                            SwapEquipment(player, type, inventory);  
                        }
                        else {
                            player.Equipments.Equip(type, inventory);
                            inventory.Clear();
                        }
                    }
                    else {
                        successfullySwapped = SwapTwoHandedStyleEquipment(sender, player, equipment, inventory);
                    }

                    if (successfullySwapped) {
                        sender.SendInventoryUpdate(player, index);
                        sender.SendEquipmentUpdate(player, type);

                        SendAttributes(sender, player); 
                    }
                }
            }
        }
    }

    private void SwapEquipment(IPlayer player, PlayerEquipmentType index, CharacterInventory inventory) {
        var equipment = player.Equipments.Get(index);

        if (equipment is not null) {
            var clone = equipment!.Clone();

            player.Equipments.Unequip(index);
            player.Equipments.Equip(index, inventory);

            inventory.Clear();
            inventory.Apply(clone);
        }
    }

    private bool SwapTwoHandedStyleEquipment(IPacketSender sender, IPlayer player, Equipment incomingEquipment, CharacterInventory inventory) {
        var equippedCount = 0;

        if (player.Equipments.IsEquipped(PlayerEquipmentType.Weapon)) {
            ++equippedCount;
        }

        if (player.Equipments.IsEquipped(PlayerEquipmentType.Shield)) {
            ++equippedCount;
        }

        if (incomingEquipment.Type == EquipmentType.Weapon) {
            // Realize a single swap.
            if (equippedCount == 1) {
                return SwapFromOneEquipmentToTwoHandedStyle(player, inventory);
            }
            else if (equippedCount == 2) {
                return SwapFromTwoEquipmentToTwoHandedStyle(sender, player, inventory);
            }
        }
        else if (incomingEquipment.Type == EquipmentType.Shield) {
            // Swap when we are using a two handed style.
            return SwapFromTwoHandedStyleToShield(sender, player, inventory);
        }

        return false;
    }

    private bool SwapFromOneEquipmentToTwoHandedStyle(IPlayer player, CharacterInventory inventory) {
        CharacterEquipment? clone = null;
        CharacterEquipment? equipment;

        if (player.Equipments.IsEquipped(PlayerEquipmentType.Weapon)) {
            equipment = player.Equipments.Get(PlayerEquipmentType.Weapon);

            if (equipment is not null) {
                clone = equipment!.Clone();

                player.Equipments.Unequip(PlayerEquipmentType.Weapon);
                player.Equipments.Equip(PlayerEquipmentType.Weapon, inventory);
            }
        }
        else if (player.Equipments.IsEquipped(PlayerEquipmentType.Shield)) {
            equipment = player.Equipments.Get(PlayerEquipmentType.Shield);

            if (equipment is not null) {
                clone = equipment!.Clone();

                player.Equipments.Unequip(PlayerEquipmentType.Shield);
                player.Equipments.Equip(PlayerEquipmentType.Weapon, inventory);
            }
        }

        if (clone is not null) {
            inventory.Clear();
            inventory.Apply(clone);

            return true;
        }

        return false;
    }

    private bool SwapFromTwoEquipmentToTwoHandedStyle(IPacketSender sender, IPlayer player, CharacterInventory inventory) {
        if (player.Equipments.IsEquipped(PlayerEquipmentType.Weapon)) {
            if (player.Equipments.IsEquipped(PlayerEquipmentType.Shield)) {
                var maximum = player.Character.MaximumInventories;
                var free = player.Inventories.FindFreeInventory(maximum);

                // Find a free slot to put shield.
                if (free is not null) {
                    var weapon = player.Equipments.Get(PlayerEquipmentType.Weapon);

                    // Swap weapons.
                    if (weapon is not null) {
                        var clone = weapon.Clone();

                        player.Equipments.Unequip(PlayerEquipmentType.Weapon);
                        player.Equipments.Equip(PlayerEquipmentType.Weapon, inventory);

                        inventory.Clear();
                        inventory.Apply(clone);

                        // It doesnt need to update, returning true tells to update it.
                        // sender.SendInventoryUpdate(player, inventory.InventoryIndex);
                        // sender.SendEquipmentUpdate(player, PlayerEquipmentType.Weapon);
                    }

                    // Put shield to an inventory's slot.
                    var shield = player.Equipments.Get(PlayerEquipmentType.Shield);

                    if (shield is not null) {
                        free.Apply(shield);

                        player.Equipments.Unequip(PlayerEquipmentType.Shield);

                        // Here we need to update it.
                        sender.SendInventoryUpdate(player, free.InventoryIndex);
                        sender.SendEquipmentUpdate(player, PlayerEquipmentType.Shield);
                    }

                    return true;
                }
                else {
                    sender.SendMessage(SystemMessage.InventoryFull, QbColor.BrigthRed, player);
                }
            }
        }

        return false;
    }

    private bool SwapFromTwoHandedStyleToShield(IPacketSender sender, IPlayer player, CharacterInventory inventory) {
        if (player.Equipments.IsEquipped(PlayerEquipmentType.Weapon)) {
            var equipment = player.Equipments.Get(PlayerEquipmentType.Weapon);

            if (equipment is not null) {
                var clone = equipment!.Clone();

                player.Equipments.Unequip(PlayerEquipmentType.Weapon);
                player.Equipments.Equip(PlayerEquipmentType.Shield, inventory);

                sender.SendEquipmentUpdate(player, PlayerEquipmentType.Weapon);

                inventory.Clear();
                inventory.Apply(clone);

                return true;
            }
        }
         
        return false;
    }

    #endregion

    #region Unequip Item

    public void UnequipItem(IPlayer player, PlayerEquipmentType index) {
        var sender = GetPacketSender();
        var equipment = player.Equipments.Get(index);

        if (equipment is not null) {
            if (equipment.ItemId > 0) {
                var maximum = player.Character.MaximumInventories;
                var inventory = player.Inventories.FindFreeInventory(maximum);

                if (inventory is not null) {
                    if (index == PlayerEquipmentType.Costume) {
                        player.Character.CostumeModel = 0;

                        var instance = GetInstance(player);

                        if (instance is not null) {
                            sender.SendPlayerModel(player, instance);
                        }
                    }

                    inventory.Apply(equipment);
                    player.Equipments.Unequip(index);

                    sender.SendInventoryUpdate(player, inventory.InventoryIndex);
                    sender.SendEquipmentUpdate(player, index);

                    // Update shield in case of two handed style.
                    if (index == PlayerEquipmentType.Weapon) {
                        sender.SendEquipmentUpdate(player, PlayerEquipmentType.Shield);
                    }

                    SendAttributes(sender, player);
                }
                else {
                    sender.SendMessage(SystemMessage.InventoryFull, QbColor.Red, player);
                }
            }
        }
    }

    #endregion

    public void DestroyItem(IPlayer player, int index) {
        var inventory = player.Inventories.FindByIndex(index);

        if (inventory is not null) {
            inventory.Clear();

            GetPacketSender().SendInventoryUpdate(player, index);
        }
    }

    private Item? GetItemFromInventory(IPlayer player, int index) {
        var inventory = player.Inventories.FindByIndex(index);

        if (inventory is not null) {
            var items = GetDatabaseItem();

            var itemId = inventory.ItemId;

            items.TryGet(itemId, out var item);

            return item;
        }

        return null;
    }

    private bool CanRevealItemAttribute(IPacketSender sender, IPlayer player, CharacterInventory inventory, Item item) {
        var equipments = GetDatabaseEquipment();

        var equipmentId = item.EquipmentId;
        var success = false;

        equipments.TryGet(equipmentId, out var equipment);

        if (equipment is not null) {
            var random = new Random();

            var attempts = 0;
            var count = 0;

            if (equipment.Attributes.Count > 0) {
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
                    sender.SendMessage(SystemMessage.SuccessToRevealItemAttribute, QbColor.Red, player);
                }
                else {
                    sender.SendMessage(SystemMessage.FailedToRevealItemAttribute, QbColor.Red, player);
                }
            }
            else {
                sender.SendMessage(SystemMessage.CannotRevealItemAttribute, QbColor.Red, player);
            }
        }

        return success;
    }

    private Equipment? GetEquipmentFromItem(Item item) {
        if (item.EquipmentId > 0) {
            var equipments = GetDatabaseEquipment();

            equipments.TryGet(item.EquipmentId, out var equipment);

            return equipment;
        }

        return null;
    }

    private Item? GetItemFromEquipped(IPlayer player, PlayerEquipmentType type) {
        var equipped = player.Equipments.Get(type);

        if (equipped is not null) {
           if (equipped.ItemId > 0) {
                var items = GetDatabaseItem();

                items.TryGet(equipped.ItemId, out var item);

                return item;
            }
        }

        return null;
    }

    private PlayerEquipmentType GetPlayerEquipmentType(IPlayer player, EquipmentType type) => type switch {
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
        EquipmentType.Earring => GetEquipmentFreeIndex(player, EquipmentType.Earring),
        EquipmentType.Ring => GetEquipmentFreeIndex(player, EquipmentType.Ring),
        EquipmentType.Bracelet => GetEquipmentFreeIndex(player, EquipmentType.Bracelet),
        EquipmentType.Costume => PlayerEquipmentType.Costume,

        _ => PlayerEquipmentType.None
    };

    private PlayerEquipmentType GetEquipmentFreeIndex(IPlayer player, EquipmentType type) {
        var index = PlayerEquipmentType.None;

        // Check if there're empty slots.
        if (type == EquipmentType.Earring) {
            if (player.Equipments.IsEquipped(PlayerEquipmentType.Earring_1) == false) {
                index = PlayerEquipmentType.Earring_1;
            }
            else if (player.Equipments.IsEquipped(PlayerEquipmentType.Earring_2) == false) {
                index = PlayerEquipmentType.Earring_2;
            }

            // Case else, swap the 1st slot.
            if (index == PlayerEquipmentType.None) {
                index = PlayerEquipmentType.Earring_1;
            }

            return index;
        }

        if (type == EquipmentType.Earring) {
            if (player.Equipments.IsEquipped(PlayerEquipmentType.Ring_1) == false) {
                index = PlayerEquipmentType.Ring_1;
            }
            else if (player.Equipments.IsEquipped(PlayerEquipmentType.Ring_2) == false) {
                index = PlayerEquipmentType.Ring_2;
            }

            if (index == PlayerEquipmentType.None) {
                index = PlayerEquipmentType.Ring_1;
            }

            return index;
        }

        if (type == EquipmentType.Bracelet) {
            if (player.Equipments.IsEquipped(PlayerEquipmentType.Bracelet_1) == false) {
                index = PlayerEquipmentType.Bracelet_1;
            }
            else if (player.Equipments.IsEquipped(PlayerEquipmentType.Bracelet_2) == false) {
                index = PlayerEquipmentType.Bracelet_2;
            }

            if (index == PlayerEquipmentType.None) {
                index = PlayerEquipmentType.Bracelet_1;
            }

            return index;
        }

        return index;
    }

    private void ChangeAndSendModel(IPacketSender sender, IPlayer player, Equipment equipment) {
        player.Character.CostumeModel = equipment.ModelId;

        var instance = GetInstance(player);

        if (instance is not null) {
            sender.SendPlayerModel(player, instance);
        }
    }

    private void SendAttributes(IPacketSender sender, IPlayer player) {
        player.AllocateAttributes();

        sender.SendAttributes(player);

        var instance = GetInstance(player);

        if (instance is not null) {
            sender.SendPlayerVital(player, instance);
        }
        else {
            sender.SendPlayerVital(player);
        }
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player.Character.Map;
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private bool IsNeededTwoHandedStyleSwap(IPlayer player, Equipment equipment) {
        if (equipment.Type == EquipmentType.Weapon) {
            if (equipment.HandStyle == EquipmentHandStyle.TwoHanded) {
                return player.Equipments.IsEquipped(PlayerEquipmentType.Shield);
            }
        }
        else if (equipment.Type == EquipmentType.Shield) {
            return IsTwoHandedStyleWeaponEquipped(player);
        }

        return false;
    }

    private bool IsTwoHandedStyleWeaponEquipped(IPlayer player) {
        if (player.Equipments.IsEquipped(PlayerEquipmentType.Weapon)) {
            var item = GetItemFromEquipped(player, PlayerEquipmentType.Weapon);

            if (item is not null) {
                var equipment = GetEquipmentFromItem(item);

                if (equipment is not null) {
                    return equipment.HandStyle == EquipmentHandStyle.TwoHanded;
                }
            }
        }

        return false;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Item> GetDatabaseItem() {
        return ContentService!.Items!;
    }

    private IDatabase<Equipment> GetDatabaseEquipment() {
        return ContentService!.Equipments!;
    }
}