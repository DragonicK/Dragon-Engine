using Dragon.Maps.Common;

namespace Dragon.Maps.Model;

public class ImageBlock {
    public Bitmap Texture { get; set; }

    public ImageBlock() {
        var size = Constants.TileSize;
        Texture = new Bitmap(size, size);
    }

    public ImageBlock Clone() {
        return new ImageBlock() {
            Texture = new Bitmap(Texture)
        };
    }
}