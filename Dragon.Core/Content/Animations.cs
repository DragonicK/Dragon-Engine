using Dragon.Core.Model.Achievements;
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
                var animation = new Animation() {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Sound = reader.ReadString()
                };

                animation.LowerFrame.Sprite = reader.ReadInt32();
                animation.LowerFrame.FrameCount = reader.ReadInt32();
                animation.LowerFrame.LoopCount = reader.ReadInt32();
                animation.LowerFrame.LoopTime = reader.ReadInt32();
                animation.LowerFrame.OffsetX = reader.ReadInt32();
                animation.LowerFrame.OffsetY = reader.ReadInt32();
                animation.LowerFrame.Width = reader.ReadInt32();
                animation.LowerFrame.Height = reader.ReadInt32();

                animation.UpperFrame.Sprite = reader.ReadInt32();
                animation.UpperFrame.FrameCount = reader.ReadInt32();
                animation.UpperFrame.LoopCount = reader.ReadInt32();
                animation.UpperFrame.LoopTime = reader.ReadInt32();
                animation.UpperFrame.OffsetX = reader.ReadInt32();
                animation.UpperFrame.OffsetY = reader.ReadInt32();
                animation.UpperFrame.Width = reader.ReadInt32();
                animation.UpperFrame.Height = reader.ReadInt32();

                values.Add(animation.Id, animation);
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
            var animation = ordered[i];

            writer.Write(animation.Id);
            writer.Write(animation.Name);
            writer.Write(animation.Sound);

            writer.Write(animation.LowerFrame.Sprite);
            writer.Write(animation.LowerFrame.FrameCount);
            writer.Write(animation.LowerFrame.LoopCount);
            writer.Write(animation.LowerFrame.LoopTime);
            writer.Write(animation.LowerFrame.OffsetX);
            writer.Write(animation.LowerFrame.OffsetY);
            writer.Write(animation.LowerFrame.Width);
            writer.Write(animation.LowerFrame.Height);

            writer.Write(animation.UpperFrame.Sprite);
            writer.Write(animation.UpperFrame.FrameCount);
            writer.Write(animation.UpperFrame.LoopCount);
            writer.Write(animation.UpperFrame.LoopTime);
            writer.Write(animation.UpperFrame.OffsetX);
            writer.Write(animation.UpperFrame.OffsetY);
            writer.Write(animation.UpperFrame.Width);
            writer.Write(animation.UpperFrame.Height);
        }
    }
}