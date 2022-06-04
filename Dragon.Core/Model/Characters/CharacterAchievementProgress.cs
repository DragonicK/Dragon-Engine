namespace Dragon.Core.Model.Characters;

public class CharacterAchievementProgress {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int AchievementId { get; set; }
    public int AchievementValue { get; set; }
}