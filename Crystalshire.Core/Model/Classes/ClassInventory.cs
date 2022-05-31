namespace Crystalshire.Core.Model.Classes;

public struct ClassInventory {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public bool Bound { get; set; }
}