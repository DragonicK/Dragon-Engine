using Crystalshire.Core.Model.Crafts;

namespace Crystalshire.Core.Model.Recipes;

public class Recipe {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CraftType CraftType { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
    public RecipeItem Reward { get; set; }
    public IList<RecipeItem> Required { get; set; }

    public Recipe() {
        Name = string.Empty;
        Description = string.Empty;
        Reward = new RecipeItem();
        Required = new List<RecipeItem>();
    }

    public override string ToString() {
        return Name;
    }
}