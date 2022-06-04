namespace Dragon.Core.Model.Recipes;

public struct RecipeItem {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
}