using Crystalshire.Core.Model.Quests;

namespace Crystalshire.Core.Content {
    public class Quests : Database<Quest> {

        public override void Load() {
            var path = $"{Folder}/{FileName}";

            if (File.Exists(path)) {
                using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
                using var reader = new BinaryReader(file);

                var count = reader.ReadInt32();

                for (var i = 0; i < count; i++) {
                    var quest = new Quest() {
                        Id = reader.ReadInt32(),
                        Title = reader.ReadString(),
                        Summary = reader.ReadString(),
                        Type = (QuestType)reader.ReadInt32(),
                        Repeatable = (QuestRepeatable)reader.ReadInt32(),
                        Shareable = (QuestShareable)reader.ReadInt32(),
                        SelectableReward = (QuestSelectableReward)reader.ReadInt32(),
                        SelectableRewardCount = reader.ReadInt32()
                    };

                    var stepCount = reader.ReadInt32();

                    for (var x = 0; x < stepCount; ++x) {
                        var step = new QuestStep() {
                            Title = reader.ReadString(),
                            Summary = reader.ReadString(),
                            ActionType = (QuestActionType)reader.ReadInt32()
                        };

                        step.Requirement.EntityId = reader.ReadInt32();
                        step.Requirement.Value = reader.ReadInt32();
                        step.Requirement.X = reader.ReadInt32();
                        step.Requirement.Y = reader.ReadInt32();

                        quest.Steps.Add(step);
                    }

                    var rewardCount = reader.ReadInt32();

                    for (var y = 0; y < rewardCount; y++) {
                        var reward = new QuestReward() {
                            Id = reader.ReadInt32(),
                            Value = reader.ReadInt32(),
                            Level = reader.ReadInt32(),
                            Bound = reader.ReadBoolean(),
                            AttributeId = reader.ReadInt32(),
                            UpgradeId = reader.ReadInt32(),
                            Type = (QuestRewardType)reader.ReadInt32()
                        };

                        quest.Rewards.Add(reward);
                    }

                    values.Add(quest.Id, quest);
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
                var quest = ordered[i];

                writer.Write(quest.Id);
                writer.Write(quest.Title);
                writer.Write(quest.Summary);
                writer.Write((int)quest.Type);
                writer.Write((int)quest.Repeatable);
                writer.Write((int)quest.Shareable);
                writer.Write((int)quest.SelectableReward);
                writer.Write(quest.SelectableRewardCount);

                writer.Write(quest.Steps.Count);
 
                for (var x = 0; x < quest.Steps.Count; ++x) {
                    writer.Write(quest.Steps[i].Title);
                    writer.Write(quest.Steps[i].Summary);
                    writer.Write((int)quest.Steps[i].ActionType);
                    writer.Write(quest.Steps[i].Requirement.EntityId);
                    writer.Write(quest.Steps[i].Requirement.Value);
                    writer.Write(quest.Steps[i].Requirement.X);
                    writer.Write(quest.Steps[i].Requirement.Y);
                }

                writer.Write(quest.Rewards.Count);

                for (var y = 0; y < quest.Rewards.Count; y++) {
                    writer.Write(quest.Rewards[i].Id);
                    writer.Write(quest.Rewards[i].Value);
                    writer.Write(quest.Rewards[i].Level);
                    writer.Write(quest.Rewards[i].Bound);
                    writer.Write(quest.Rewards[i].AttributeId);
                    writer.Write(quest.Rewards[i].UpgradeId);
                    writer.Write((int)quest.Rewards[i].Type);
                }
            }
        }
    }
}