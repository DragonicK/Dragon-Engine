namespace Crystalshire.Maps.Editor;

public interface IGrid {
    Pen Pen { get; set; }
    Bitmap GetGrid();
    void Update();
    void Update(int width, int height);
}