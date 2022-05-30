namespace Crystalshire.Maps.Model;

public struct Fog {
    public int Id { get; set; }
    public Blending Blending { get; set; }
    public byte Opacity { get; set; }
    public byte Red { get; set; }
    public byte Green { get; set; }
    public byte Blue { get; set; }
}