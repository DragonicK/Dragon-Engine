namespace Crystalshire.Maps.Images;

public class Tileset : ITileset {
    private readonly string _folder;
    private readonly IDictionary<string, Bitmap> _tiles;

    private const int NameIndex = 0;
    private const int ExtensionIndex = 1;

    public int Count => _tiles.Count;

    public Tileset(string folder) {
        _folder = folder;
        _tiles = new Dictionary<string, Bitmap>();
    }

    public bool Exists(string id) {
        return _tiles.ContainsKey(id);
    }

    public Bitmap? Get(string id) {
        if (_tiles.ContainsKey(id)) {
            return _tiles[id];
        }

        return null;
    }

    public void Load() {
        var files = Directory.GetFiles(_folder);

        foreach (var file in files) {
            if (GetFileExtension(file) == "png") {
                _tiles.Add(GetFileName(file), new Bitmap(file));
            }
        }
    }

    private string GetNameAndExtension(string fullPath) {
        var texts = fullPath.Split('\\');
        return texts[texts.Length - 1];
    }

    private string GetFileName(string fullPath) {
        var names = GetNameAndExtension(fullPath).Split('.');

        return names[NameIndex];
    }

    private string GetFileExtension(string fullPath) {
        var names = GetNameAndExtension(fullPath).Split('.');

        return names[ExtensionIndex];
    }
}
