using Crystalshire.Core.Model.Equipments;

namespace Crystalshire.Core.Content;

public class Equipments : Database<Equipment> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var equipment = new Equipment {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Type = (EquipmentType)reader.ReadInt32(),
                    HandStyle = (EquipmentHandStyle)reader.ReadInt32(),
                    Proficiency = (EquipmentProficiency)reader.ReadInt32(),
                    ModelId = reader.ReadInt32(),
                    UpgradeId = reader.ReadInt32(),
                    EquipmentSetId = reader.ReadInt32(),
                    DisassembleId = reader.ReadInt32(),
                    MaximumSocket = reader.ReadInt32(),
                    BaseAttackSpeed = reader.ReadInt32(),
                    AttackAnimationId = reader.ReadInt32(),
                };

                var skillCount = reader.ReadInt32();

                for (var n = 0; n < skillCount; n++) {
                    equipment.Skills.Add(new EquipmentSkill() {
                        Id = reader.ReadInt32(),
                        Level = reader.ReadInt32(),
                        UnlockAtLevel = reader.ReadInt32()
                    });
                }

                var attributeCount = reader.ReadInt32();

                for (var n = 0; n < attributeCount; n++) {
                    equipment.Attributes.Add(new EquipmentAttribute() {
                        AttributeId = reader.ReadInt32(),
                        Chance = reader.ReadInt32()
                    });
                }

                Add(equipment.Id, equipment);
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
            writer.Write((int)equipment.Type);
            writer.Write((int)equipment.HandStyle);
            writer.Write((int)equipment.Proficiency);
            writer.Write(equipment.ModelId);
            writer.Write(equipment.UpgradeId);
            writer.Write(equipment.EquipmentSetId);
            writer.Write(equipment.DisassembleId);
            writer.Write(equipment.MaximumSocket);
            writer.Write(equipment.BaseAttackSpeed);
            writer.Write(equipment.AttackAnimationId);

            var skillCount = equipment.Skills.Count;

            writer.Write(skillCount);

            for (var n = 0; n < skillCount; n++) {
                writer.Write(equipment.Skills[n].Id);
                writer.Write(equipment.Skills[n].Level);
                writer.Write(equipment.Skills[n].UnlockAtLevel);
            }

            var attributeCount = equipment.Attributes.Count;

            writer.Write(attributeCount);

            for (var n = 0; n < attributeCount; n++) {
                writer.Write(equipment.Attributes[n].AttributeId);
                writer.Write(equipment.Attributes[n].Chance);
            }
        }
    }

}