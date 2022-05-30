namespace Crystalshire.Maps.Export;

internal class ExportData {
    public int Id { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Length { get; set; }
    public byte[]? Data { get; set; }
}