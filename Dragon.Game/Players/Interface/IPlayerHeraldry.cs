using Dragon.Core.Content;
using Dragon.Core.Model.Items;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.Heraldries;
using Dragon.Core.Model.Attributes;

namespace Dragon.Game.Players;

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