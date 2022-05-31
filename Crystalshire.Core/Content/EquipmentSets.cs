using Crystalshire.Core.Model.EquipmentSets;

namespace Crystalshire.Core.Content;

public class EquipmentSets : Database<EquipmentSet> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var equipmentSet = new EquipmentSet() {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString()
                };

                var pieces = reader.ReadInt32();

                for (var n = 0; n < pieces; ++n) {
                    var index = (EquipmentSetCount)reader.ReadInt32();

                    var effect = new EquipmentSetEffect() {
                        AttributeId = reader.ReadInt32(),
                        SkillId = reader.ReadInt32()
                    };

                    equipmentSet.Sets[index] = effect;
                }

                Add(equipmentSet.Id, equipmentSet);
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
            var equipment = ordered[i];

            writer.Write(equipment.Id);
            writer.Write(equipment.Name);
            writer.Write(equipment.Description);

            writer.Write(equipment.Sets.Count);

            foreach (var (index, effect) in equipment.Sets) {
                writer.Write((int)index);
                writer.Write(effect.AttributeId);
                writer.Write(effect.SkillId);
            }
        }
    }
}