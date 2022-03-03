using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Attributes;

namespace Crystalshire.Core.Content {
    public class Attributes : Database<GroupAttribute> {

        public override void Load() {
            var path = $"{Folder}/{FileName}";

            if (!File.Exists(path)) {
                return;
            }

            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);
            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var attribute = new SingleAttribute();

                var attributes = new GroupAttribute {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString()
                };

                for (var n = 0; n < Enum.GetValues<Vital>().Length; n++) {
                    attribute = new SingleAttribute() {
                        Value = reader.ReadSingle(),
                        Percentage = reader.ReadBoolean()
                    };

                    attributes.Vital[(Vital)n] = attribute;
                }

                for (var n = 0; n < Enum.GetValues<PrimaryAttribute>().Length; n++) {
                    attribute = new SingleAttribute() {
                        Value = reader.ReadSingle(),
                        Percentage = reader.ReadBoolean()
                    };

                    attributes.Primary[(PrimaryAttribute)n] = attribute;
                }

                for (var n = 0; n < Enum.GetValues<SecondaryAttribute>().Length; n++) {
                    attribute = new SingleAttribute() {
                        Value = reader.ReadSingle(),
                        Percentage = reader.ReadBoolean()
                    };

                    attributes.Secondary[(SecondaryAttribute)n] = attribute;
                }

                for (var n = 0; n < Enum.GetValues<UniqueAttribute>().Length; n++) {
                    attributes.Unique[(UniqueAttribute)n] = reader.ReadSingle();
                }

                for (var n = 0; n < Enum.GetValues<ElementAttribute>().Length; n++) {
                    attribute = new SingleAttribute() {
                        Value = reader.ReadSingle(),
                        Percentage = reader.ReadBoolean()
                    };

                    attributes.ElementAttack[(ElementAttribute)n] = attribute;

                    attribute = new SingleAttribute() {
                        Value = reader.ReadSingle(),
                        Percentage = reader.ReadBoolean()
                    };

                    attributes.ElementDefense[(ElementAttribute)n] = attribute;
                }

                Add(attributes.Id, attributes);
            }
        }

        public override void Save() {
            var path = $"{Folder}/{FileName}";

            using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
            using var writer = new BinaryWriter(file);

            writer.Write(values.Count);

            var ordered = values.Select(p => p.Value).OrderBy(p => p.Id).ToList();

            for (var i = 0; i < ordered.Count; i++) {
                var attributes = ordered[i];

                writer.Write(attributes.Id);
                writer.Write(attributes.Name);
                writer.Write(attributes.Description);

                for (var n = 0; n < Enum.GetValues<Vital>().Length; n++) {
                    writer.Write(attributes.Vital[(Vital)n].Value);
                    writer.Write(attributes.Vital[(Vital)n].Percentage);
                }

                for (var n = 0; n < Enum.GetValues<PrimaryAttribute>().Length; n++) {
                    writer.Write(attributes.Primary[(PrimaryAttribute)n].Value);
                    writer.Write(attributes.Primary[(PrimaryAttribute)n].Percentage);
                }

                for (var n = 0; n < Enum.GetValues<SecondaryAttribute>().Length; n++) {
                    writer.Write(attributes.Secondary[(SecondaryAttribute)n].Value);
                    writer.Write(attributes.Secondary[(SecondaryAttribute)n].Percentage);
                }

                for (var n = 0; n < Enum.GetValues<UniqueAttribute>().Length; n++) {
                    writer.Write(attributes.Unique[(UniqueAttribute)n]);
                }

                for (var n = 0; n < Enum.GetValues<ElementAttribute>().Length; n++) {
                    writer.Write(attributes.ElementAttack[(ElementAttribute)n].Value);
                    writer.Write(attributes.ElementAttack[(ElementAttribute)n].Percentage);

                    writer.Write(attributes.ElementDefense[(ElementAttribute)n].Value);
                    writer.Write(attributes.ElementDefense[(ElementAttribute)n].Percentage);
                }
            }
        }
    }
}