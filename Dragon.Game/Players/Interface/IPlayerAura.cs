using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerAura {
    int Count { get; }
    bool Contains(int id);
    void Add(int id, int level, int range);
    void Remove(int id);
    IList<CharacterAura> ToList();
}