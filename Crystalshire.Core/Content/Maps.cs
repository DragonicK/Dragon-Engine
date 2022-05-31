using Crystalshire.Core.Model.Maps;

namespace Crystalshire.Core.Content;

public class Maps : Database<IMap> {

    public override void Load() {
        if (Directory.Exists(Folder)) {
            var files = Directory.GetFiles(Folder, "*.dat");

            LoadInstances(files);
        }

        var folders = GetFolders(Folder);

        if (folders?.Length > 0) {
            foreach (var folder in folders) {
                LoadInstances(GetFiles(folder));
            }
        }
    }

    private void LoadInstances(string[]? files) {
        if (files is not null) {
            if (files.Length > 0) {
                foreach (var file in files) {
                    LoadInstance(file);
                }
            }
        }
    }

    private void LoadInstance(string file) {
        using var fs = new FileStream(file, FileMode.Open, FileAccess.Read);
        using var binary = new BinaryReader(fs);

        var map = new Map {
            Id = binary.ReadInt32(),
            Name = binary.ReadString(),
            Music = binary.ReadString(),
            Ambience = binary.ReadString(),

            Moral = (Moral)binary.ReadByte(),
            Weather = (Weather)binary.ReadByte(),

            MaximumX = binary.ReadInt32(),
            MaximumY = binary.ReadInt32()
        };

        map.Tile = new Tile[map.MaximumX, map.MaximumY];

        for (var x = 0; x < map.MaximumX; x++) {
            for (var y = 0; y < map.MaximumY; y++) {
                map.Tile[x, y] = new Tile {
                    Type = (TileType)binary.ReadByte(),
                    Data1 = binary.ReadInt32(),
                    Data2 = binary.ReadInt32(),
                    Data3 = binary.ReadInt32(),
                    Data4 = binary.ReadInt32(),
                    Data5 = binary.ReadInt32(),
                    DirBlock = binary.ReadByte()
                };
            }
        }

        values.Add(map.Id, map);
    }
}