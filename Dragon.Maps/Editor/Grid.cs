using Dragon.Maps.Common;

namespace Dragon.Maps.Editor;

public class Grid : IGrid {
    public Pen Pen { get; set; }

    private Bitmap _grid;
    private int _width;
    private int _height;

    public Grid() {
        Pen = Pens.White;
        _grid = new Bitmap(1, 1);
    }

    public Bitmap GetGrid() {
        return _grid;
    }

    public void Update() {
        var g = Graphics.FromImage(_grid);
        var size = Constants.TileSize;

        g.Clear(Color.Transparent);

        for (var x = 0; x < _width + 1; ++x) {
            for (var y = 0; y < _height + 1; ++y) {
                g.DrawLine(Pen, 0, y * size, _grid.Width, y * size);  // Horizontal.         
                g.DrawLine(Pen, x * size, 0, x * size, _grid.Height); // Vertical. 
            }
        }
    }

    public void Update(int width, int height) {
        var size = Constants.TileSize;

        _width = width;
        _height = height;

        _grid = new Bitmap(width * size, height * size);

        Update();
    }
}