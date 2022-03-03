namespace Crystalshire.Core.Model.Achievements {
    public class Achievement {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
        public Rarity Rarity { get; set; }
        public int Point { get; set; }
        public int AttributeId { get; set; }
        public AchievementCategory Category { get; set; }
        public AchievementRequirementEntry Entry { get; set; }
        public AchievementReward Reward { get; set; }

        public Achievement() {
            Name = string.Empty;
            Description = string.Empty;
            Reward = new AchievementReward();
            Entry = new AchievementRequirementEntry();
        }

        public override string ToString() {
            return Name;
        }
    }
}