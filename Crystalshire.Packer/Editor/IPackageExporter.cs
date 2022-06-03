namespace Crystalshire.Packer.Editor;

public interface IPackageExporter {
    EventHandler<IPackageArgs> OnProgressChanged { get; set; }
    void Export(string file, int[] indexes, IPackage package);
    void Export(string file, IPackage package);
    void Export(string file, IPackageFile packed);
}