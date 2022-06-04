namespace Crystalshire.Packer.Editor;

public interface IPackageFile {
    string Name { get; set; }
    string Extension { get; set; }
    string State { get; set; }
    byte[] Bytes { get; set; }
    int Length { get; set; }
    int Width { get; set; }
    int Height { get; set; }
}