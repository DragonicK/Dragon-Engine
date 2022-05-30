using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Titles;
using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public interface IPlayerTitle {
    IEntityAttribute Attributes { get; }
    IDatabase<Title>? Titles { get; set; }
    IDatabase<GroupAttribute>? TitleAttributes { get; set; }
    int Count { get; }
    bool Add(int id);
    bool Remove(int id);
    int GetId(int index);
    bool Contains(int id);
    int[] ToArrayId();
    void Equip(int id);
    void Unequip();
    IList<CharacterTitle> ToList();
}