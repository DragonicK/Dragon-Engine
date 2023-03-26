namespace Dragon.Game.Combat.Common;

public struct SkillOverTime
{
    public int Id { get; set; }
    public int Value { get; set; }
    public int Duration { get; set; }
    public int Interval { get; set; }
    public int Seconds { get; set; }
    public int CasterId { get; set; }
    public SkillOverTimeType Type { get; set; }

    public void Clear()
    {
        Id = 0;
        Value = 0;
        Duration = 0;
        Interval = 0;
        Seconds = 0;
        CasterId = 0;
    }
}