using Dragon.Core.Model;

namespace Dragon.Network.Messaging.DTO;

public struct DataShopItem {
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public CurrencyType CurrencyId { get; set; }
    public int Price { get; set; }
}