using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerRecipe {
    int Count { get; }
    bool Add(int id);
    bool Remove(int id);
    int GetId(int index);
    bool Contains(int id);
    int[] ToArrayId();
    IList<CharacterRecipe> ToList();
}