using Dragon.Core.Model;
using Dragon.Core.Model.Premiums;
using Dragon.Core.Serialization;

namespace Dragon.Core.Content;

public class Premiums : Database<Premium> {

    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadPremiums(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadPremiums(GetFiles(folder));
            }
        }

        if (processed == 0) {
            SaveDefault();
        }
    }

    private int LoadPremiums(string[]? files) {
        var count = 0;

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

                    count++;
                }
            }
        }

        return count;
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