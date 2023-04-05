namespace Dragon.Core.Model.Chests;

public class ChestItem {
    public int ChestId { get; set; }
    public ChestContentType ContentType { get; set; }
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public double Chance { get; set; }
}