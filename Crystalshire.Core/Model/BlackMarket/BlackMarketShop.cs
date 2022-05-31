namespace Crystalshire.Core.Model.BlackMarket;

public class BlackMarketShop {
    public IDictionary<BlackMarketItemCategory, BlackMarketCategory> Categories { get; set; }

    public BlackMarketShop(int maximumPageItems) {
        Categories = new Dictionary<BlackMarketItemCategory, BlackMarketCategory>();

        var categories = Enum.GetValues<BlackMarketItemCategory>();

        foreach (var category in categories) {
            Categories.Add(category, new BlackMarketCategory(maximumPageItems));
        }
    }

    public void Add(BlackMarketItem item) {
        Categories[item.Category].Add(item);
    }

    public void Clear() {
        Categories.Clear();
    }

    public BlackMarketItem? GetItem(int id) {
        foreach (var (_, category) in Categories) {
            var item = category.Find(id);

            if (item is not null) {
                return item;
            }
        }

        return null;
    }

    public int GetPageCount(BlackMarketItemCategory category) {
        return Categories[category].GetPageCount();
    }

    public BlackMarketItem[]? GetItems(BlackMarketItemCategory category, int page) {
        return Categories[category].GetItems(page);
    }
}