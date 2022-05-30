using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public interface IPlayerRecipe {
    int Count { get; }
    bool Add(int id);
    bool Remove(int id);
    int GetId(int index);
    bool Contains(int id);
    int[] ToArrayId();
    IList<CharacterRecipe> ToList();
}