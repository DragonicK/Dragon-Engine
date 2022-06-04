namespace Dragon.Core.Model.Quests;

public class QuestReward {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public QuestRewardType Type { get; set; }
}