namespace Crystalshire.Maps.Editor;

public class Colors {
    public SolidBrush ForeBlock { get; set; }
    public SolidBrush ForeWarp { get; set; }
    public SolidBrush ForeTrap { get; set; }
    public SolidBrush ForeAvoid { get; set; }
    public SolidBrush ForeChat { get; set; }
    public SolidBrush BackBlock { get; set; }
    public SolidBrush BackWarp { get; set; }
    public SolidBrush BackTrap { get; set; }
    public SolidBrush BackAvoid { get; set; }
    public SolidBrush BackChat { get; set; }

    public Colors() {
        ForeBlock = new SolidBrush(Color.FromArgb(60, Color.Red));
        ForeWarp = new SolidBrush(Color.FromArgb(60, Color.SkyBlue));
        ForeTrap = new SolidBrush(Color.FromArgb(60, Color.Magenta));
        ForeAvoid = new SolidBrush(Color.FromArgb(60, Color.Gold));
        ForeChat = new SolidBrush(Color.FromArgb(60, Color.Aquamarine));

        BackBlock = new SolidBrush(Color.FromArgb(60, Color.Red));
        BackWarp = new SolidBrush(Color.FromArgb(60, Color.SkyBlue));
        BackTrap = new SolidBrush(Color.FromArgb(60, Color.Magenta));
        BackAvoid = new SolidBrush(Color.FromArgb(60, Color.Gold));
        BackChat = new SolidBrush(Color.FromArgb(60, Color.Aquamarine));
    }
}