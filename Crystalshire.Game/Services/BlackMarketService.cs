using Crystalshire.Core.Logs;
using Crystalshire.Core.Services;
using Crystalshire.Core.Serialization;
using Crystalshire.Core.Model.BlackMarket;

namespace Crystalshire.Game.Services {
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

            if (files.Length > 0) {
                LoadBlackMarket(files);
            }
            else {
                SaveDefault();
            }
        }

        public void Stop() {

        }

        public void Update(int deltaTime) {

        }

        private void LoadBlackMarket(string[] files) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var item = Json.Get<BlackMarketItem>(file);

                    if (item is not null) {
                        if (item.Id != 0 && item.ItemId != 0) {
                            BlackMarket!.Add(item);
                            Json.Save(file, item);
                        }
                    }
                }
            }
        }

        private void SaveDefault() {
            Json.Save($"{Folder}/default.json", new BlackMarketItem() { Id = 0 });
        }
    }
}