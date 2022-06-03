namespace Crystalshire.Packer.Editor;

public interface IPackageHandler {
    EventHandler<IPackageArgs> OnProgressChanged { get; set; }
    PackageOperation Open(string file, string passphrase, IPackage package);
    PackageOperation Save(string file, string passphrase, IPackage package);
}