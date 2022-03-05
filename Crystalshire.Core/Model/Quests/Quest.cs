namespace Crystalshire.Core.Model.Quests {
    public class Quest {
        public int Id { get; set; }
        public string Title { get; set; } 
        public string Summary { get; set; }
        public QuestType Type { get; set; }
        public QuestRepeatable Repeatable { get; set; }
        public QuestShareable Shareable { get; set; }
        public QuestSelectableReward SelectableReward { get; set; }
        public int SelectableRewardCount { get; set; }
        public IList<QuestStep> Steps { get; set; }
        public IList<QuestReward> Rewards { get; set; }

        public Quest() {
            Title = string.Empty;
            Summary = string.Empty;
            Steps = new List<QuestStep>();
            Rewards = new List<QuestReward>();
        }

        public override string ToString() {
            return Title;
        }
    }
}