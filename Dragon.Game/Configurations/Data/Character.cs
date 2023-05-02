
namespace Dragon.Game.Configurations.Data;

public sealed class Character {
    public int Maximum { get; set; }
    public bool Create { get; set; }
    public bool Delete { get; set; }
    public int DeleteMinimumLevel { get; set; }
    public int DeleteMaximumLevel { get; set; }
    public int MinimumNameLength { get; set; }
    public int MaximumNameLength { get; set; }
    public List<LevelRangeExclusion> DeletionLevelRanges { get; set; }

    public Character() {
        Maximum = 3;
        Create = true;
        Delete = true;
        DeleteMinimumLevel = 1;
        DeleteMaximumLevel = 50;

        MinimumNameLength = 4;
        MaximumNameLength = 15;

        DeletionLevelRanges = new List<LevelRangeExclusion>() {
                new LevelRangeExclusion() {
                    Minimum = 1,
                    Maximum = 50,
                    Minutes = 5
                }
            };
    }

    public LevelRangeExclusion GetDeletionRange(int level) {
        return DeletionLevelRanges
            .FirstOrDefault(x => level >= x.Minimum && level <= x.Maximum);
    }
}