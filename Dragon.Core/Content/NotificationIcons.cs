using Dragon.Core.Model.NotificationIcons;

namespace Dragon.Core.Content;

public class NotificationIcons : Database<NotificationIcon> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var icon = new NotificationIcon() {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString(),
                    IconId = reader.ReadInt32(),
                    IconType = (NotificationIconType)reader.ReadInt32()
                };

                values.Add(icon.Id, icon);
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
            writer.Write(ordered[i].Id);
            writer.Write(ordered[i].Name);
            writer.Write(ordered[i].Description);
            writer.Write(ordered[i].IconId);
            writer.Write((int)ordered[i].IconType);
        }
    }
}