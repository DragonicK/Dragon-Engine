namespace Crystalshire.Maps.Images;
public interface ITileset {
    int Count { get; }
    void Load();
    bool Exists(string id);
    Bitmap? Get(string id);
}