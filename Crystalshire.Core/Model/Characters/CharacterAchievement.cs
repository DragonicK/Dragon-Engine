namespace Crystalshire.Core.Model.Characters;

public class CharacterAchievement {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int AchievementId { get; set; }
    public DateTime AchievementDate { get; set; }
}