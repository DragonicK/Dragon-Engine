namespace Crystalshire.Packer.Editor;

public interface IPackage {
    int Count { get; }
    IPackageFile this[int index] { get; set; }
    void Add(IPackageFile file);
    void Clear();
    void Insert(int index, IPackageFile file);
    void Remove(int[] indexes);
    bool MoveUp(int[] indexes);
    bool MoveDown(int[] indexes);
    bool MoveTo(int index, int[] indexes);
    List<IPackageFile> ToList();
}