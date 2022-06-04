using Dragon.Core.Model.Crafts;
using Dragon.Core.Model.Recipes;

namespace Dragon.Core.Content;

public class Recipes : Database<Recipe> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var recipe = new Recipe {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString(),
                    CraftType = (CraftType)reader.ReadInt32(),
                    Level = reader.ReadInt32(),
                    Experience = reader.ReadInt32(),

                    Reward = new RecipeItem() {
                        Id = reader.ReadInt32(),
                        Value = reader.ReadInt32(),
                        Level = reader.ReadInt32(),
                        Bound = reader.ReadBoolean()
                    }
                };

                var requiredCount = reader.ReadInt32();

                for (var n = 0; n < requiredCount; n++) {
                    var item = new RecipeItem() {
                        Id = reader.ReadInt32(),
                        Value = reader.ReadInt32(),
                        Level = reader.ReadInt32(),
                        Bound = reader.ReadBoolean()
                    };

                    recipe.Required.Add(item);
                }

                Add(recipe.Id, recipe);
            }
        }
    }

    public override void Save() {
        var path = $"{Folder}/{FileName}";

        using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(file);

        writer.Write(values.Count);

        var ordered = values.Select(p => p.Value).OrderBy(p => p.Id).ToList();

        for (var i = 0; i < ordered.Count; ++i) {
            var recipe = ordered[i];

            writer.Write(recipe.Id);
            writer.Write(recipe.Name);
            writer.Write(recipe.Description);
            writer.Write((int)recipe.CraftType);
            writer.Write(recipe.Level);
            writer.Write(recipe.Experience);

            writer.Write(recipe.Reward.Id);
            writer.Write(recipe.Reward.Value);
            writer.Write(recipe.Reward.Level);
            writer.Write(recipe.Reward.Bound);

            writer.Write(recipe.Required.Count);

            for (var n = 0; n < recipe.Required.Count; ++n) {
                var item = recipe.Required[n];

                writer.Write(item.Id);
                writer.Write(item.Value);
                writer.Write(item.Level);
                writer.Write(item.Bound);
            }
        }
    }
}