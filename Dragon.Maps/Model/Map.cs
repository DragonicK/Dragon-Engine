using Dragon.Maps.Common;

namespace Dragon.Maps.Model;

public class Map : IMap {
    public IProperty Property { get; set; }
    public string LastPath { get; set; }
    public bool Resizing { get; set; }
    public ImageBlock[,] Ground { get; set; }
    public ImageBlock[,] Mask1 { get; set; }
    public ImageBlock[,] Mask2 { get; set; }
    public ImageBlock[,] Fringe1 { get; set; }
    public ImageBlock[,] Fringe2 { get; set; }
    public Tile[,] Attribute { get; set; }

    public Map() {
        Property = new Property();

        LastPath = string.Empty;

        var width = Property.Width;
        var height = Property.Height;

        Ground = new ImageBlock[width, height];
        Mask1 = new ImageBlock[width, height];
        Mask2 = new ImageBlock[width, height];
        Fringe1 = new ImageBlock[width, height];
        Fringe2 = new ImageBlock[width, height];
        Attribute = new Tile[width, height];

        for (var x = 0; x < width; ++x) {
            for (var y = 0; y < height; ++y) {
                Ground[x, y] = new ImageBlock();
                Mask1[x, y] = new ImageBlock();
                Mask2[x, y] = new ImageBlock();
                Fringe1[x, y] = new ImageBlock();
                Fringe2[x, y] = new ImageBlock();
                Attribute[x, y] = new Tile();
            }
        }
    }

    public void Apply(TileType tileType, int x, int y) {
        if (x >= Property.Width || y >= Property.Height) {
            return;
        }

        Attribute[x, y].Type = tileType;
    }

    public void Apply(Terrain terrain, int x, int y, int sourceX, int sourceY, Bitmap tilset) {
        if (x >= Property.Width || y >= Property.Height) {
            return;
        }

        Bitmap? texture = null;

        switch (terrain) {
            case Terrain.Ground:
                texture = Ground[x, y].Texture;
                break;

            case Terrain.Mask1:
                texture = Mask1[x, y].Texture;
                break;

            case Terrain.Mask2:
                texture = Mask2[x, y].Texture;
                break;

            case Terrain.Fringe1:
                texture = Fringe1[x, y].Texture;
                break;

            case Terrain.Fringe2:
                texture = Fringe2[x, y].Texture;
                break;
        }

        if (texture is not null) {
            var block = Constants.TileSize;

            var g = Graphics.FromImage(texture);

            g.Clear(Color.Transparent);

            g.DrawImage(tilset, new Rectangle(0, 0, block, block), new Rectangle(sourceX, sourceY, block, block), GraphicsUnit.Pixel);
        }
    }

    public void Clear(Terrain terrain, int x, int y) {
        if (x >= Property.Width || y >= Property.Height) {
            return;
        }

        Bitmap? texture = null;

        switch (terrain) {
            case Terrain.Ground:
                texture = Ground[x, y].Texture;
                break;

            case Terrain.Mask1:
                texture = Mask1[x, y].Texture;
                break;

            case Terrain.Mask2:
                texture = Mask2[x, y].Texture;
                break;

            case Terrain.Fringe1:
                texture = Fringe1[x, y].Texture;
                break;

            case Terrain.Fringe2:
                texture = Fringe2[x, y].Texture;
                break;
        }

        if (texture is not null) {
            var g = Graphics.FromImage(texture);

            g.Clear(Color.Transparent);
        }
    }

    public void UpdateSize() {
        Resizing = true;

        var oldWidth = Ground.GetUpperBound(0);
        var oldHeight = Ground.GetUpperBound(1);

        var width = Property.Width;
        var height = Property.Height;

        var ground = new ImageBlock[width, height];
        var mask1 = new ImageBlock[width, height];
        var mask2 = new ImageBlock[width, height];
        var fringe1 = new ImageBlock[width, height];
        var fringe2 = new ImageBlock[width, height];

        var attributes = new Tile[width, height];

        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                ground[x, y] = new ImageBlock();
                mask1[x, y] = new ImageBlock();
                mask2[x, y] = new ImageBlock();
                fringe1[x, y] = new ImageBlock();
                fringe2[x, y] = new ImageBlock();
                attributes[x, y] = new Tile();

                // Copia os objetos para os novo tiles.
                if (x <= oldWidth && y <= oldHeight) {
                    ground[x, y] = Ground[x, y].Clone();
                    mask1[x, y] = Mask1[x, y].Clone();
                    mask2[x, y] = Mask2[x, y].Clone();
                    fringe1[x, y] = Fringe1[x, y].Clone();
                    fringe2[x, y] = Fringe2[x, y].Clone();
                    attributes[x, y] = Attribute[x, y].Clone();
                }
            }
        }

        Ground = ground;
        Mask1 = mask1;
        Mask2 = mask2;
        Fringe1 = fringe1;
        Fringe2 = fringe2;
        Attribute = attributes;

        Resizing = false;
    }
}