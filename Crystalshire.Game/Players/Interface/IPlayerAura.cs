using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public interface IPlayerAura {
    int Count { get; }
    bool Contains(int id);
    void Add(int id, int level, int range);
    void Remove(int id);
    IList<CharacterAura> ToList();
}