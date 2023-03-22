namespace Dragon.Core.Model.Animations;

public class AnimationFrame {
    public int Sprite { get; set; }
    public int FrameCount { get; set; }
    public int LoopCount { get; set; }
    public int OffsetX { get; set; }
    public int OffsetY { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int LoopTime { get; set; }

    public AnimationFrame() {
        Width = 192;
        Height = 192;
    }
}