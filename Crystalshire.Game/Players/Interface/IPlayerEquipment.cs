using Crystalshire.Core.Content;
using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Model.Equipments;
using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.EquipmentSets;

namespace Crystalshire.Game.Players;

public interface IPlayerEquipment {
    IEntityAttribute Attributes { get; }
    IDatabase<Item>? Items { get; set; }
    IDatabase<Equipment>? Equipments { get; set; }
    IDatabase<GroupAttribute>? EquipmentAttributes { get; set; }
    IDatabase<GroupAttribute>? EquipmentUpgrades { get; set; }
    IDatabase<EquipmentSet>? EquipmentSets { get; set; }
    IDatabase<GroupAttribute>? EquipmentSetAttributes { get; set; }

    CharacterEquipment? Get(PlayerEquipmentType index);
    bool IsEquipped(PlayerEquipmentType index);
    void Equip(PlayerEquipmentType index, CharacterInventory inventory);
    void Unequip(PlayerEquipmentType index);
    void UpdateAttributes();
    IList<CharacterEquipment> ToList();
}