using Dragon.Core.Logs;
using Dragon.Core.Services;
using Dragon.Core.Serialization;
using Dragon.Core.Model.BlackMarket;

namespace Dragon.Game.Services;

public class BlackMarketService : IService, IUpdatableService {
    public ServicePriority Priority => ServicePriority.Last;
    public ILogger? Logger { get; private set; }
    public BlackMarketShop? BlackMarket { get; private set; }
    public ConfigurationService? Configuration { get; private set; }

    private const string Folder = "./Server/BlackMarket";

    public void Start() {
        var maximum = Configuration!.BlackMarket.MaximumItemPerPage;

        BlackMarket = new BlackMarketShop(maximum);

        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadBlackMarket(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadBlackMarket(GetFiles(folder));
            }
        }

        if (processed == 0) {
            SaveDefault();
        }
    }

    public void Stop() {

    }

    public void Update(int deltaTime) {

    }

    private int LoadBlackMarket(string[]? files) {
        var processed = 0;

        if (files is not null) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var item = Json.Get<BlackMarketItem>(file);

                    if (item is not null) {
                        if (item.Id != 0 && item.ItemId != 0) {
                            BlackMarket!.Add(item);
                            Json.Save(file, item);

                            processed++;
                        }
                    }
                }
            }
        }

        return processed;
    }

    private void SaveDefault() {
        Json.Save($"{Folder}/Pet/1 Default Item.json", new BlackMarketItem() { Id = 1, Category = BlackMarketItemCategory.Pet });
        Json.Save($"{Folder}/Boost/2 Default Item.json", new BlackMarketItem() { Id = 2, Category = BlackMarketItemCategory.Boost });
        Json.Save($"{Folder}/Supply/3 Default Item.json", new BlackMarketItem() { Id = 3, Category = BlackMarketItemCategory.Supply });
        Json.Save($"{Folder}/Service/4 Default Item.json", new BlackMarketItem() { Id = 4, Category = BlackMarketItemCategory.Service });
        Json.Save($"{Folder}/Package/5 Default Item.json", new BlackMarketItem() { Id = 5, Category = BlackMarketItemCategory.Package });
        Json.Save($"{Folder}/Promotional/6 Default Item.json", new BlackMarketItem() { Id = 6, Category = BlackMarketItemCategory.Promo });
        Json.Save($"{Folder}/Consumable/7 Default Item.json", new BlackMarketItem() { Id = 7, Category = BlackMarketItemCategory.Consumable });
    }

    private string[]? GetFolders(string root) {
        return Directory.GetDirectories(root);
    }

    private string[]? GetFiles(string folder) {
        if (Directory.Exists(folder)) {
            return Directory.GetFiles(folder);
        }

        return null;
    }
}