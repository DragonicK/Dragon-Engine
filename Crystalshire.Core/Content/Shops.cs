using Crystalshire.Core.Serialization;
using Crystalshire.Core.Model.Shops;

namespace Crystalshire.Core.Content;

public class Shops : Database<Shop> {

    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadShopItem(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadShopItem(GetFiles(folder));
            }
        }
        if (processed == 0) {
            SaveDefault();
        }
    }

    private int LoadShopItem(string[]? files) {
        var count = 0;

        if (files is not null) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var item = Json.Get<ShopItem>(file);

                    if (item is not null) {
                        if (item.Id != 0 && item.ShopId != 0) {
                            AddShopItem(item);
                            Json.Save(file, item);
                        }
                    }

                    count++;
                }
            }
        }

        return count;
    }

    private void AddShopItem(ShopItem item) {
        var shopId = item.ShopId;

        if (!Contains(shopId)) {
            values.Add(shopId, new Shop() {
                Id = item.ShopId,
                Name = item.ShopName
            });
        }

        var shop = values[shopId];

        shop.Items.Add(item);
    }

    private void SaveDefault() {
        var item = new ShopItem() {
            Id = 2,
            Value = 1,
            Level = 1,
            Bound = false,
            AttributeId = 1,
            UpgradeId = 1,
            ShopId = 1,
            ShopName = "NOME",
            Price = 1000,
        };

        Json.Save($"{Folder}/default.json", item);
    }
}