namespace Dragon.Maps.Model;

public interface IMap {
    string LastPath { get; set; }
    bool Resizing { get; set; }
    IProperty Property { get; set; }
    ImageBlock[,] Ground { get; set; }
    ImageBlock[,] Mask1 { get; set; }
    ImageBlock[,] Mask2 { get; set; }
    ImageBlock[,] Fringe1 { get; set; }
    ImageBlock[,] Fringe2 { get; set; }
    Tile[,] Attribute { get; set; }

    void UpdateSize();
    void Apply(TileType tileType, int x, int y);
    void Apply(Terrain terrain, int x, int y, int sourceX, int sourceY, Bitmap tilset);
    void Clear(Terrain terrain, int x, int y);
}