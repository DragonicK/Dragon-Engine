﻿using Dragon.Core.Model.Chests;
using Dragon.Core.Serialization;

namespace Dragon.Core.Content;

public class Chests : Database<Chest>  {

    private const string FileInformation = "Chests.json";
   
    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        LoadChestSprites();

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
                if (!file.Contains(FileInformation)) {
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

            if (!Contains(id)) {
                values.Add(id, chest!);
            }
        }
    }

    private void LoadChestSprites() {
        var file = $"./Server/{FileInformation}";

        if (!File.Exists(file)) {
            Json.Save(file, new List<ChestSprite> {
                new ChestSprite() { Id = 1, Sprite = 1 },
                new ChestSprite() { Id = 2, Sprite = 2 },
                new ChestSprite() { Id = 3, Sprite = 3 },
                new ChestSprite() { Id = 4, Sprite = 4 },
                new ChestSprite() { Id = 5, Sprite = 5 }
            });
        }

        var chests = Json.Get<List<ChestSprite>>(file);

        chests?.ForEach(x => {
            var id = x.Id;
            var sprite = x.Sprite;

            var chest = Contains(id) ? this[id] : new Chest() { Id = id, Sprite = sprite };

            if (chest is not null) {
                if (Contains(id)) {
                    chest.Id = id;
                    chest.Sprite = sprite;
                }
                else {
                    values.Add(id, chest);
                }
            }
        });
    }

    private void SaveDefault() {
        Directory.CreateDirectory("./Server/Chests/Chest #1");
        Json.Save($"{Folder}/Chest #1/Golden Sword.json", new ChestItem() { ChestId = 1 });

        Directory.CreateDirectory("./Server/Chests/Chest #2");
        Json.Save($"{Folder}/Chest #2/Hyperion Staff.json", new ChestItem() { ChestId = 2 });
    }
}