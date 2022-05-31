namespace Crystalshire.Core.Model.Shops;

public class Shop {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ShopItem> Items { get; set; }

    public Shop() {
        Name = string.Empty;
        Items = new List<ShopItem>();
    }
}