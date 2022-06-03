using Crystalshire.Core.Cryptography;

namespace Crystalshire.Packer.Editor;

public class PackageHandler : IPackageHandler {
    public EventHandler<IPackageArgs> OnProgressChanged { get; set; }

    private const string DefaultText = "This is a text!";
    private const string SecurityText = "This is a complement";

    private readonly AesManaged _aes;

    public PackageHandler(EventHandler<IPackageArgs> onProgressChanged) {
        _aes = new AesManaged();
        OnProgressChanged = onProgressChanged;
    }

    public PackageOperation Open(string file, string passphrase, IPackage package) {
        package.Clear();

        return PackageOperation.Success;
    }

    public PackageOperation Save(string file, string passphrase, IPackage package) {
        var stream = new FileStream(file, FileMode.Create, FileAccess.Write);
        var writer = new BinaryWriter(stream);

        WritePassword(passphrase, writer);
        WriteFiles(passphrase, package, writer);

        return PackageOperation.Success;
    }

    private void WriteFiles(string passphrase, IPackage package, BinaryWriter writer) {
        var maximum = package.Count;
        var counter = 0;

        writer.Write(package.Count);

        var files = package.ToList();

        files.ForEach(x => WriteFile(ref counter, maximum, passphrase, x, writer));
    }

    private void WriteFile(ref int counter, int maximum, string passphrase, IPackageFile file, BinaryWriter writer) {
        var hash = Hash.Compute(passphrase + SecurityText);
        var key = Hash.Compute(hash, hash.Length, true);
        var iv = Hash.Compute(hash, hash.Length, false);

        var encrypted = _aes.Encrypt(file.Bytes, key, iv);

        writer.Write(file.Name);
        writer.Write(file.Extension);
        writer.Write(file.Width);
        writer.Write(file.Height);

        writer.Write(encrypted.Length);
        writer.Write(encrypted);

        UpdateProgress(file.Name, ++counter, maximum);
    }

    private void WritePassword(string passphrase, BinaryWriter writer) {
        var hash = Hash.Compute(DefaultText);

        var _passphrase = Hash.Compute(passphrase + SecurityText);
        var key = Hash.Compute(_passphrase, hash.Length, true);
        var iv = Hash.Compute(_passphrase, hash.Length, false);

        var encrypted = _aes.Encrypt(hash, key, iv);

        writer.Write(encrypted.Length);
        writer.Write(encrypted);
    }

    private bool CheckPassphrase(string passphrase, BinaryReader reader) {
        var length = reader.ReadInt32();
        var encrypted = reader.ReadBytes(length);

        var hash = Hash.Compute(passphrase + SecurityText);
        var key = Hash.Compute(hash, hash.Length, true);
        var iv = Hash.Compute(hash, hash.Length, false);

        var decrypted = _aes.Decrypt(encrypted, key, iv);

        if (decrypted is not null) {
            var _hash = Hash.Compute(DefaultText);

            if (_hash.Length != decrypted.Length) {
                return false;
            }

            if (!CheckEquality(_hash, decrypted)) {
                return false;
            }
        }

        return decrypted is not null;
    }

    private bool CheckEquality(byte[] first, byte[] second) {
        return Enumerable.SequenceEqual(first, second);
    }

    private void UpdateProgress(string name, int count, int maximum) {
        OnProgressChanged?.Invoke(this, new PackageArgs() {
            Name = name,
            Count = count,
            Maximum = maximum
        });
    }
}