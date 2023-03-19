namespace Dragon.Network.Messaging.DTO;

public struct DataRate {
    public int Character { get; set; }
    public int Talent { get; set; }
    public int Party { get; set; }
    public int Guild { get; set; }
    public int Skill { get; set; }
    public int Craft { get; set; }
    public int Quest { get; set; }
    public int Pet { get; set; }
    public int GoldChance { get; set; }
    public int GoldIncrease { get; set; }
    public int[] ItemDrop { get; set; }
}