using Dragon.Core.Content;
using Dragon.Core.Model;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.Equipments;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.EquipmentSets;

namespace Dragon.Game.Players;

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