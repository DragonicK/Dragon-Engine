namespace Dragon.Game.Configurations.Data;

public struct LevelRangeExclusion {
    public int Minimum { get; set; }
    public int Maximum { get; set; }
    public int Minutes { get; set; }

    public bool IsEmpty() {
        return Minimum == 0 && Maximum == 0 && Minutes == 0;
    }
}