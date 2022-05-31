namespace Crystalshire.Core.Model.Achievements;

public class Achievement {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Rarity Rarity { get; set; }
    public int Point { get; set; }
    public int AttributeId { get; set; }
    public AchievementCategory Category { get; set; }
    public IList<AchievementRequirementEntry> Requirements { get; set; }
    public IList<AchievementReward> Rewards { get; set; }

    public Achievement() {
        Name = string.Empty;
        Description = string.Empty;
        Requirements = new List<AchievementRequirementEntry>();
        Rewards = new List<AchievementReward>();
    }

    public override string ToString() {
        return Name;
    }
}