using Crystalshire.Core.Serialization;
using Crystalshire.Core.Model.Shops;

namespace Crystalshire.Core.Content {
    public class Shops : Database<Shop> {

        private int ProcessedFiles = 0;

        public override void Load() {
            var files = Directory.GetFiles(Folder);

            if (files.Length > 0) {
                LoadShopItem(files);
            }

            var folders = GetFolders(Folder);

            if (folders?.Length > 0) {
                foreach (var folder in folders) {
                    LoadShopItem(GetFiles(folder));
                }
            }
            if (ProcessedFiles == 0) {
                SaveDefault();
            }
        }

        private void LoadShopItem(string[]? files) {
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

                        ProcessedFiles++;
                    }
                }
            }
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

        private string[]? GetFolders(string root) {
            return Directory.GetDirectories(root);
        }

        private string[]? GetFiles(string folder) {
            if (Directory.Exists(folder)) {
                return Directory.GetFiles(folder);
            }

            return null;
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
}