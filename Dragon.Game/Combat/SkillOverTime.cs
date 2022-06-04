namespace Dragon.Game.Combat;


public struct SkillOverTime {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Duration { get; set; }
    public int Interval { get; set; }
    public int Seconds { get; set; }
    public int CasterId { get; set; }
    public SkillOverTimeType Type { get; set; }
}