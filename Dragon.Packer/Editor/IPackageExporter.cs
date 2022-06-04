namespace Dragon.Packer.Editor;

public interface IPackageExporter {
    EventHandler<IPackageArgs> OnProgressChanged { get; set; }
    void Export(string path, int[] indexes, IPackage package);
    void Export(string path, IPackage package);
}