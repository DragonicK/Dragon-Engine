using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Items;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Model.Heraldries;
using Crystalshire.Core.Model.Attributes;

namespace Crystalshire.Game.Players;

public interface IPlayerHeraldry {
    IEntityAttribute Attributes { get; }
    IDatabase<Item>? Items { get; set; }
    IDatabase<Heraldry>? Heraldries { get; set; }
    IDatabase<GroupAttribute>? HeraldryAttributes { get; set; }
    IDatabase<GroupAttribute>? HeraldryUpgrades { get; set; }

    CharacterHeraldry? Get(int index);
    bool IsEquipped(int index);
    void Equip(int index, CharacterInventory inventory);
    void Unequip(int index);
    void UpdateAttributes();
    IList<CharacterHeraldry> ToList();
    CharacterHeraldry? FindFreeHeraldry(int maximum);
}