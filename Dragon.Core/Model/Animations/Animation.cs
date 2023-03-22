namespace Dragon.Core.Model.Animations;

public class Animation {
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Sound { get; set; } = string.Empty;
    public AnimationFrame UpperFrame { get; set; }
    public AnimationFrame LowerFrame { get; set; }

    public Animation() {
        UpperFrame = new AnimationFrame();
        LowerFrame = new AnimationFrame();
    }

    public override string ToString() {
        return Name;
    }
}