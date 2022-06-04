using Dragon.Core.Model.Equipments;

namespace Dragon.Core.Model.Achievements;

public sealed class AchievementRequirementEntry {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public int Count { get; set; }
    public string Description { get; set; } = string.Empty;
    public Rarity Rarity { get; set; }
    public EquipmentType Equipment { get; set; }
    public AchievementPrimaryRequirement PrimaryType { get; set; }
    public AchievementSecondaryRequirement SecondaryType { get; set; }
}