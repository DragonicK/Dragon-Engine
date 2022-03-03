using Crystalshire.Core.Model.Heraldries;

namespace Crystalshire.Core.Content {
    public class Heraldries : Database<Heraldry> {

        public override void Load() {
            var path = $"{Folder}/{FileName}";

            if (File.Exists(path)) {
                using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(file);

                var count = reader.ReadInt32();

                for (var i = 0; i < count; i++) {
                    var heraldry = new Heraldry {
                        Id = reader.ReadInt32(),
                        Name = reader.ReadString(),
                        Description = reader.ReadString(),
                        UpgradeId = reader.ReadInt32()
                    };

                    var maximum = reader.ReadInt32();

                    for (var n = 0; n < maximum; n++) {
                        var random = new HeraldryAttribute() {
                            AttributeId = reader.ReadInt32(),
                            Chance = reader.ReadInt32()
                        };

                        heraldry.Attributes.Add(random);
                    }

                    Add(heraldry.Id, heraldry);
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
                writer.Write(effect.UpgradeId);

                var maximum = effect.Attributes.Count;

                writer.Write(maximum);

                for (var n = 0; n < maximum; ++n) {
                    writer.Write(effect.Attributes[n].AttributeId);
                    writer.Write(effect.Attributes[n].Chance);
                }
            }
        }

    }
}