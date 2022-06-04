using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public class PlayerRecipe : IPlayerRecipe {
    public int Count => _recipes.Count;

    private readonly IList<CharacterRecipe> _recipes;
    private readonly long _characterId;

    public PlayerRecipe(long characterId, IList<CharacterRecipe> recipes) {
        _recipes = recipes;
        _characterId = characterId;
    }

    public bool Add(int id) {
        if (Contains(id)) {
            return false;
        }

        _recipes.Add(new CharacterRecipe() {
            CharacterId = _characterId,
            RecipeId = id
        });

        return true;
    }

    public bool Remove(int id) {
        var selected = _recipes.FirstOrDefault(p => p.RecipeId == id);

        if (selected is not null) {
            _recipes.Remove(selected);
        }

        return selected is not null;
    }

    public int GetId(int index) {
        return _recipes[index].RecipeId;
    }

    public bool Contains(int id) {
        var selected = _recipes.FirstOrDefault(p => p.RecipeId == id);

        return selected is not null;
    }

    public int[] ToArrayId() {
        return _recipes.Select(p => p.RecipeId).ToArray();
    }

    public IList<CharacterRecipe> ToList() {
        return _recipes;
    }
}