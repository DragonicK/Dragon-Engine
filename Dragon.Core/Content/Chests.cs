using Dragon.Core.Model.Chests;
using Dragon.Core.Serialization;

namespace Dragon.Core.Content;

public class Chests : Database<Chest>  {

    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadItemChests(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadItemChests(GetFiles(folder));
            }
        }

        if (processed == 0) {
            SaveDefault();
        }
    }

    private int LoadItemChests(string[]? files) {
        var count = 0;

        if (files is not null) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var item = Json.Get<ChestItem>(file);

                    if (item is not null) {
                        AddItem(item);
                        Json.Save(file, item);
                    }
                    
                    count++;
                }
            }
        }

        return count;
    }

    private void AddItem(ChestItem item) {
        var id = item.ChestId;

        if (id > 0) {
            var chest = Contains(id) ? this[id] : new Chest() { Id = id };

            if (item.Id > 0) {
                chest?.Items.Add(item);
            }
        }
    }

    private void SaveDefault() {
        Json.Save($"{Folder}/Example Region/default.json", new ChestItem() { ChestId = 1 });
        Json.Save($"{Folder}/Example Gold Chest/default.json", new ChestItem() { ChestId = 2 });
    }
}