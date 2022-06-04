namespace Dragon.Packer.Editor;

public class PackageArgs : IPackageArgs {
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Maximum { get; set; }
}