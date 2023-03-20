using Dragon.Core.Model.Animations;

namespace Dragon.Core.Content;

public class Animations : Database<Animation> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                //var achievement = new Achievement() {

                //};

                //values.Add(achievement.Id, new Animation());
            }
        }
    }

    public override void Save() {
        var path = $"{Folder}/{FileName}";

        using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(file);

        var count = values.Count;

        writer.Write(count);

        var ordered = values.Select(p => p.Value).OrderBy(p => p.Id).ToList();

        for (var i = 0; i < ordered.Count; ++i) {

        }
    }
}