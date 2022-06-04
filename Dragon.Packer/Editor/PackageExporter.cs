namespace Dragon.Packer.Editor;

public class PackageExporter : IPackageExporter {
    public EventHandler<IPackageArgs> OnProgressChanged { get; set; }

    public PackageExporter(EventHandler<IPackageArgs> onProgressChanged) {
        OnProgressChanged = onProgressChanged;
    }

    public void Export(string path, int[] indexes, IPackage package) {
        var maximum = indexes.Length;
        var counter = 0;

        var list = package.ToList(indexes);

        list.ForEach(x => Save(ref counter, maximum, path, x));
    }

    public void Export(string path, IPackage package) {
        var maximum = package.Count;
        var counter = 0;

        var list = package.ToList();

        list.ForEach(x => Save(ref counter, maximum, path, x));
    }

    private void Save(ref int counter, int maximum, string path, IPackageFile packed) {
        var name = packed.Name;
        var extension = packed.Extension;

        using var stream = new FileStream($"{path}/{name}.{extension}", FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(stream);

        writer.Write(packed.Bytes);

        UpdateProgress(name, ++counter, maximum);
    }

    private void UpdateProgress(string name, int count, int maximum) {
        OnProgressChanged?.Invoke(this, new PackageArgs() {
            Name = name,
            Count = count,
            Maximum = maximum
        });
    }
}