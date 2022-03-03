using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Premiums;
using Crystalshire.Core.Serialization;

namespace Crystalshire.Core.Content {
    public class Premiums : Database<Premium> {

        private int ProcessedFiles = 0;

        public override void Load() {
            var files = Directory.GetFiles(Folder);

            if (files.Length > 0) {
                LoadPremiums(files);
            }

            var folders = GetFolders(Folder);

            if (folders?.Length > 0) {
                foreach (var folder in folders) {
                    LoadPremiums(GetFiles(folder));
                }
            }

            if (ProcessedFiles == 0) { 
                SaveDefault();
            }
        }

        private void LoadPremiums(string[]? files) {
            if (files is not null) {
                foreach (var file in files) {
                    if (Json.FileExists(file)) {
                        var premium = Json.Get<Premium>(file);

                        if (premium is not null) {
                            if (premium.Id != 0) {
                                Add(premium.Id, premium);
                                Json.Save(file, premium);
                            }
                        }

                        ProcessedFiles++;
                    }
                }
            }
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
            var premium = new Premium() {
                Id = 0,
                Name = "Example",
                ItemDrops = new Dictionary<Rarity, float>() {
                { Rarity.Common, 1f },
                { Rarity.Uncommon, 0.9f },
                { Rarity.Rare, 0.8f },
                { Rarity.Epic, 0.7f },
                { Rarity.Mythic, 0.6f },
                { Rarity.Ancient, 0.5f },
                { Rarity.Legendary, 0.4f },
                { Rarity.Ethereal, 0.3f }
                }
            };

            Json.Save($"{Folder}/default.json", premium);
        }
    }
}
