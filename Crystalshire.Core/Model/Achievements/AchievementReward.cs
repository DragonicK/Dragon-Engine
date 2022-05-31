namespace Crystalshire.Core.Model.Achievements;

public class AchievementReward {
    public AchievementRewardType Type { get; set; }
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
}