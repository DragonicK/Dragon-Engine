using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Titles;

namespace Crystalshire.Core.Content;

public class Titles : Database<Title> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var title = new Title {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString(),
                    Rarity = (Rarity)reader.ReadInt32(),
                    AttributeId = reader.ReadInt32()
                };

                values.Add(title.Id, title);
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
            var title = ordered[i];

            writer.Write(title.Id);
            writer.Write(title.Name);
            writer.Write(title.Description);
            writer.Write((int)title.Rarity);
            writer.Write(title.AttributeId);
        }
    }
}