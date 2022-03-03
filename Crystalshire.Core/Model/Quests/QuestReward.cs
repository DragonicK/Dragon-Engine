namespace Crystalshire.Core.Model.Quests {
    public struct QuestReward {
        public int Id { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public QuestRewardType Type { get; set; }
    }
}