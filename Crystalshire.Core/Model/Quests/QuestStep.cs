namespace Crystalshire.Core.Model.Quests {
    public class QuestStep {
        public string Title { get; set; }
        public string Summary { get; set; }
        public QuestActionType ActionType { get; set; }
        public QuestRequirement Requirement { get; set; }

        public QuestStep() {
            Title = string.Empty;
            Summary = string.Empty;
            Requirement = new QuestRequirement();
        }
    }
}