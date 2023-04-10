using Dragon.Core.Model;
using Dragon.Core.Model.Chests;

namespace Dragon.Network.Messaging.DTO;

public struct DataChestItem {
    public ChestContentType ContentType { get; set; }
    public CurrencyType CurrencyType { get; set; }
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public int UpgradeId { get; set; }
    public int AttributeId { get; set; }
    public bool Bound { get; set; }
}