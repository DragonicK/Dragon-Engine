using Dragon.Core.Model.Gashas;
using Dragon.Core.Serialization;

namespace Dragon.Core.Content;

public class Gashas : Database<Gasha> {

    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadGashas(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadGashas(GetFiles(folder));
            }
        }

        if (processed == 0) {
            SaveDefault();
        }
    }

    private int LoadGashas(string[]? files) {
        var count = 0;

        if (files is not null) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var gasha = Json.Get<Gasha>(file);

                    if (gasha is not null) {
                        if (gasha.Id != 0) {
                            Add(gasha.Id, gasha);
                            Json.Save(file, gasha);
                        }
                    }

                    count++;
                }
            }
        }

        return count;
    }

    private void SaveDefault() {
        var gasha = new Gasha() {
            Id = 1
        };

        gasha.Items.Add(new GashaItem() {
            Id = 1,
            Value = 1
        });

        gasha.Items.Add(new GashaItem() {
            Id = 2,
            Value = 1
        });

        gasha.Items.Add(new GashaItem() {
            Id = 8,
            Value = 100
        });

        Json.Save($"{Folder}/default.json", gasha);
    }
}