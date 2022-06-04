namespace Dragon.Core.Model.Shops;


public class ShopItem {
    public int ShopId { get; set; }
    public string ShopName { get; set; }
    public int Id { get; set; }
    public int Value { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public CurrencyType CurrencyId { get; set; }
    public int Price { get; set; }

    public ShopItem() {
        ShopName = string.Empty;
    }
}