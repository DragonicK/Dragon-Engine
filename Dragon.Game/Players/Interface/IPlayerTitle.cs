using Dragon.Core.Content;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Titles;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

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