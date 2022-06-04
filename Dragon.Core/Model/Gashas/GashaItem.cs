namespace Dragon.Core.Model.Gashas;

public class GashaItem {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public double Chance { get; set; }
    public int Charge { get; set; }
    public byte IsPacked { get; set; }
    public byte WrappableCount { get; set; }
    public int FusionedItemId { get; set; }
    public int Socket { get; set; }
    public int FusionedSocket { get; set; }
    public short ActivationCount { get; set; }
    public int ItemSkinId { get; set; }
}