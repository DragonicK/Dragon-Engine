using Crystalshire.Core.Model;

using Crystalshire.Core.Common;

using Crystalshire.Maps.Model;
using Crystalshire.Maps.Common;
using Crystalshire.Maps.Editor;
using Crystalshire.Maps.Images;

namespace Crystalshire.Maps;

public class Starter {
    public JetBrainsMonoLoader? FontLoader { get; set; }
    public JetBrainsMono? JetBrainsMono { get; set; }
    public IGrid? Grid { get; set; }
    public ITileset? Tiles { get; set; }
    public IList<IMap>? Maps { get; set; }
    public Colors? Colors { get; set; }
    public Bitmap? TextureDirection { get; set; }
    public Point[]? DirectionPosition { get; set; }

    public void Start() {
        FontLoader = new JetBrainsMonoLoader();

        FontLoader.LoadFromResource();

        JetBrainsMono = new JetBrainsMono(FontLoader);

        CheckDirectories();

        StartMaps();
        StartGrid();
        StartTiles();
        StartColors();
        StartDirection();
        StartTexture();
    }

    private void CheckDirectories() {
        var dir = new EngineDirectory();

        dir.Add("./Tiles");
        dir.Add("./Exported");
        dir.Add("./Projects");
        dir.Add("./Exported/Png");
        dir.Add("./Exported/Engine");

        dir.Create();
    }

    private void StartMaps() {
        Maps = new List<IMap>();
    }

    private void StartGrid() {
        Grid = new Grid();
        Grid.Update(1, 1);
    }

    private void StartTiles() {
        Tiles = new Tileset("Tiles");
        Tiles.Load();
    }

    private void StartColors() {
        Colors = new Colors();
    }

    private void StartTexture() {
        TextureDirection = Properties.Resources.Direction;
    }

    private void StartDirection() {
        DirectionPosition = new Point[Constants.MaximumDirection];

        DirectionPosition[(int)Direction.Up].X = 12;
        DirectionPosition[(int)Direction.Up].Y = 0;

        DirectionPosition[(int)Direction.Down].X = 12;
        DirectionPosition[(int)Direction.Down].Y = 24;

        DirectionPosition[(int)Direction.Left].X = 0;
        DirectionPosition[(int)Direction.Left].Y = 12;

        DirectionPosition[(int)Direction.Right].X = 24;
        DirectionPosition[(int)Direction.Right].Y = 12;
    }
}