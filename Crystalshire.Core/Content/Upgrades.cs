using Crystalshire.Core.Model.Upgrades;
using Crystalshire.Core.Serialization;

namespace Crystalshire.Core.Content;

public class Upgrades : Database<Upgrade> {

    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadUpgrades(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadUpgrades(GetFiles(folder));
            }
        }

        if (processed == 0) {
            SaveDefault();
        }
    }

    private int LoadUpgrades(string[]? files) {
        var count = 0;

        if (files is not null) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var upgrade = Json.Get<Upgrade>(file);

                    if (upgrade is not null) {
                        if (upgrade.Id != 0) {
                            Add(upgrade.Id, upgrade);
                            Json.Save(file, upgrade);
                        }
                    }

                    count++;
                }
            }
        }

        return count;
    }

    private void SaveDefault() {
        var upgrade = new Upgrade() {
            Id = 0
        };

        for (var i = 1; i <= 10; ++i) {
            var list = new List<UpgradeRequirement>(2) {
                    new UpgradeRequirement() {
                        Id = 1,
                        Amount = i
                    },
                    new UpgradeRequirement() {
                        Id = 2,
                        Amount = i
                    }
                };

            upgrade.ContentLevel.Add(i, new UpgradeLevel() {
                Break = 0,
                Reduce = 0,
                Success = i * 10,
                Cost = i * 250,
                Requirements = list
            });
        }

        Json.Save($"{Folder}/default.json", upgrade);
    }
}