namespace Dragon.Core.Model.BlackMarket;

public class BlackMarketCategory {
    public IList<BlackMarketItem> Items { get; }

    private readonly int MaximumItemPage;

    public BlackMarketCategory(int maximumItemPage) {
        MaximumItemPage = maximumItemPage;

        Items = new List<BlackMarketItem>();
    }

    public void Add(BlackMarketItem item) {
        Items.Add(item);
    }

    public BlackMarketItem? Find(int id) {
        return Items.FirstOrDefault(x => x.Id == id);
    }

    public BlackMarketItem[] GetItems(int page) {
        var items = new List<BlackMarketItem>(MaximumItemPage);

        var maximum = page * MaximumItemPage;
        var minimum = (page * MaximumItemPage) - MaximumItemPage;

        if (Items.Count <= maximum) {
            maximum = Items.Count;
        }

        for (var i = minimum; i < maximum; ++i) {
            items.Add(Items[i]);
        }

        return items.ToArray();
    }

    public int GetPageCount() {
        var count = Items.Count / MaximumItemPage;
        var mod = Items.Count % MaximumItemPage;

        if (mod > 0) {
            count++;
        }

        return count;
    }
}