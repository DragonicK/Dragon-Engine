using Crystalshire.Core.Model.Effects;

namespace Crystalshire.Core.Content {
    public class Effects : Database<Effect>{

        public override void Load() {
            var path = $"{Folder}/{FileName}";

            if (File.Exists(path)) {
                using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(file);

                var count = reader.ReadInt32();

                for (var i = 0; i < count; i++) {
                    var effect = new Effect {
                        Id = reader.ReadInt32(),
                        Name = reader.ReadString(),
                        Description = reader.ReadString(),
                        EffectType = (EffectType)reader.ReadInt32(),
                        IconId = reader.ReadInt32(),
                        Duration = reader.ReadInt32(),
                        Dispellable = reader.ReadBoolean(),
                        RemoveOnDeath = reader.ReadBoolean(),
                        Unlimited = reader.ReadBoolean(),
                        AttributeId = reader.ReadInt32(),
                        UpgradeId = reader.ReadInt32(),
                        Override = (EffectOverride)reader.ReadInt32()
                    };

                    Add(effect.Id, effect);
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
                var effect = ordered[i];

                writer.Write(effect.Id);
                writer.Write(effect.Name);
                writer.Write(effect.Description);
                writer.Write((int)effect.EffectType);
                writer.Write(effect.IconId);
                writer.Write(effect.Duration);
                writer.Write(effect.Dispellable);
                writer.Write(effect.RemoveOnDeath);
                writer.Write(effect.Unlimited);
                writer.Write(effect.AttributeId);
                writer.Write(effect.UpgradeId);
                writer.Write((int)effect.Override);
            }
        }

    }
}