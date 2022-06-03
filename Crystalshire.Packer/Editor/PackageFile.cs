namespace Crystalshire.Packer.Editor;

public class PackageFile : IPackageFile {
    public string Name { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
    public byte[] Bytes { get; set; } = Array.Empty<byte>();
    public int Length { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}