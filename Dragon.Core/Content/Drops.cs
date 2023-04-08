using Dragon.Core.Model.Chests;
using Dragon.Core.Model.Drops;
using Dragon.Core.Serialization;

namespace Dragon.Core.Content;

public class Drops : Database<Drop> {
    public override void Load() {
        var files = Directory.GetFiles(Folder);
        var processed = 0;

        if (files.Length > 0) {
            processed += LoadNpcDrops(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                processed += LoadNpcDrops(GetFiles(folder));
            }
        }

        if (processed == 0) {
            SaveDefault();
        }
    }

    private int LoadNpcDrops(string[]? files) {
        var count = 0;

        if (files is not null) {
            foreach (var file in files) {
                if (Json.FileExists(file)) {
                    var drop = Json.Get<Drop>(file);

                    if (drop is not null) {
                        AddDrop(drop);
                        Json.Save(file, drop);
                    }

                    count++;
                }
            }
        }

        return count;
    }

    private void AddDrop(Drop drop) {
        var npcId = drop.NcpId;

        if (npcId > 0) {
            var added = Contains(npcId) ? this[npcId] : new Drop() { NcpId = npcId };

            added?.Chests.AddRange(drop.Chests);

            if (!Contains(npcId)) {
                values.Add(npcId, drop);
            }
        }
    }

    private void SaveDefault() {
        Json.Save($"{Folder}/Npc #1 Drops.json", new Drop() { NcpId = 1, Chests = new List<int>() { 1, 2, 3, 4, 5 } });
        Json.Save($"{Folder}/Npc #2 Drops.json", new Drop() { NcpId = 2, Chests = new List<int>() { 1, 2, 3, 4, 5 } });
    }
}