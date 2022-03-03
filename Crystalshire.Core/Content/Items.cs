using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Items;

namespace Crystalshire.Core.Content {
    public class Items : Database<Item> {

        public override void Load() {
            var path = $"{Folder}/{FileName}";

            if (File.Exists(path)) {
                using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(file);

                var count = reader.ReadInt32();

                for (var i = 0; i < count; i++) {
                    var item = new Item {
                        Id = reader.ReadInt32(),
                        Name = reader.ReadString(),
                        Description = reader.ReadString(),
                        Sound = reader.ReadString(),
                        IconId = reader.ReadInt32(),
                        Type = (ItemType)reader.ReadInt32(),
                        Rarity = (Rarity)reader.ReadInt32(),
                        Bind = (BindType)reader.ReadInt32(),
                        RequiredLevel = reader.ReadInt32(),
                        Price = reader.ReadInt32(),
                        MaximumStack = reader.ReadInt32(),
                        GashaBoxId = reader.ReadInt32(),
                        RecipeId = reader.ReadInt32(),
                        EquipmentId = reader.ReadInt32(),
                        SkillId = reader.ReadInt32(),
                        Cooldown = reader.ReadInt32(),
                        Interval = reader.ReadInt32(),
                        Duration = reader.ReadInt32(),
                        EffectId = reader.ReadInt32(),
                        EffectLevel = reader.ReadInt32(),
                        EffectDuration = reader.ReadInt32(),
                        ClassCode = reader.ReadInt32(),
                        UpgradeId = reader.ReadInt32(),
                        MaximumLevel = reader.ReadInt32()
                    };

                    var length = Enum.GetValues(typeof(Vital)).Length;
                    for (var n = 0; n < length; n++) {
                        item.Vital[n] = reader.ReadInt32();
                    }

                    Add(item.Id, item);
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
                var item = ordered[i];

                writer.Write(item.Id);
                writer.Write(item.Name);
                writer.Write(item.Description);
                writer.Write(item.Sound);
                writer.Write(item.IconId);
                writer.Write((int)item.Type);
                writer.Write((int)item.Rarity);
                writer.Write((int)item.Bind);
                writer.Write(item.RequiredLevel);
                writer.Write(item.Price);
                writer.Write(item.MaximumStack);
                writer.Write(item.GashaBoxId);
                writer.Write(item.RecipeId);
                writer.Write(item.EquipmentId);
                writer.Write(item.SkillId);
                writer.Write(item.Cooldown);
                writer.Write(item.Interval);
                writer.Write(item.Duration);
                writer.Write(item.EffectId);
                writer.Write(item.EffectLevel);
                writer.Write(item.EffectDuration);
                writer.Write(item.ClassCode);
                writer.Write(item.UpgradeId);
                writer.Write(item.MaximumLevel);

                var length = Enum.GetValues(typeof(Vital)).Length;
                for (var n = 0; n < length; n++) {
                    writer.Write(item.Vital[n]);
                }

            }
        }
    }
}