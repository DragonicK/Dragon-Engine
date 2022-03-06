using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Equipments;
using Crystalshire.Core.Model.Achievements;

namespace Crystalshire.Core.Content {
    public class Achievements : Database<Achievement> {

        public override void Load() {
            var path = $"{Folder}/{FileName}";

            if (File.Exists(path)) {
                using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(file);

                var count = reader.ReadInt32();

                for (var i = 0; i < count; i++) {
                    var achievement = new Achievement() {
                        Id = reader.ReadInt32(),
                        Name = reader.ReadString(),
                        Description = reader.ReadString(),
                        Rarity = (Rarity)reader.ReadInt32(),
                        Point = reader.ReadInt32(),
                        AttributeId = reader.ReadInt32(),
                        Category = (AchievementCategory)reader.ReadInt32()
                    };

                    var requirementCount = reader.ReadInt32();

                    for (var n = 0; n < requirementCount; ++n) {
                        var entry = new AchievementRequirementEntry() {
                            Id = reader.ReadInt32(),
                            Value = reader.ReadInt32(),
                            Level = reader.ReadInt32(),
                            Count = reader.ReadInt32(),
                            Rarity = (Rarity)reader.ReadInt32(),
                            Equipment = (EquipmentType)reader.ReadInt32(),
                            PrimaryType = (AchievementPrimaryRequirement)reader.ReadInt32(),
                            SecondaryType = (AchievementSecondaryRequirement)reader.ReadInt32()
                        };

                        achievement.Requirements.Add(entry);
                    }

                    var rewardCount = reader.ReadInt32();

                    for (var n = 0; n < rewardCount; n++) {
                        var reward = new AchievementReward() {
                            Id = reader.ReadInt32(),
                            Value = reader.ReadInt32(),
                            Level = reader.ReadInt32(),
                            Bound = reader.ReadBoolean(),
                            AttributeId = reader.ReadInt32(),
                            UpgradeId = reader.ReadInt32(),
                        };

                        achievement.Rewards.Add(reward);
                    }

                    values.Add(achievement.Id, achievement);
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
                var achievement = ordered[i];

                writer.Write(achievement.Id);
                writer.Write(achievement.Name);
                writer.Write(achievement.Description);
                writer.Write((int)achievement.Rarity);
                writer.Write(achievement.Point);
                writer.Write(achievement.AttributeId);
                writer.Write((int)achievement.Category);

                writer.Write(achievement.Requirements.Count);

                for (var n = 0; n < achievement.Requirements.Count; n++) {
                    var entry = achievement.Requirements[n];

                    writer.Write(entry.Id);
                    writer.Write(entry.Value);
                    writer.Write(entry.Level);
                    writer.Write(entry.Count);
                    writer.Write((int)entry.Rarity);
                    writer.Write((int)entry.Equipment);
                    writer.Write((int)entry.PrimaryType);
                    writer.Write((int)entry.SecondaryType);
                }

                writer.Write(achievement.Rewards.Count);

                for (var n = 0; n < achievement.Rewards.Count; n++) {
                    var reward = achievement.Rewards[n];

                    writer.Write(reward.Id);
                    writer.Write(reward.Value);
                    writer.Write(reward.Level);
                    writer.Write(reward.Bound);
                    writer.Write(reward.AttributeId);
                    writer.Write(reward.UpgradeId);
                }
            }
        }
    }
}