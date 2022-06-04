namespace Dragon.Packer.Editor;

public interface IPackageArgs {
    string Name { get; set; }
    int Count { get; set; }
    int Maximum { get; set; }
}