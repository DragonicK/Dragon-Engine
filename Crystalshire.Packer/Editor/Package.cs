namespace Crystalshire.Packer.Editor;

public class Package : IPackage {
    public IPackageFile this[int index] {
        get => _files[index];
        set => _files[index] = value;
    }

    public int Count => _files.Count;

    private readonly List<IPackageFile> _files;

    public Package() {
        _files = new List<IPackageFile>();
    }

    public void Add(IPackageFile file) {
        _files.Add(file);
    }

    public void Clear() {
        _files.Clear();
    }

    public void Insert(int index, IPackageFile file) {
        _files.Insert(index, file);
    }

    public bool MoveDown(int[] indexes) {
        if (indexes.Length > 0) {
            if (indexes[0] == Count - 1) {
                return false;
            }

            var index = indexes[0] + 1;

            return MoveTo(ref index, indexes);
        }

        return false;
    }

    public bool MoveTo(ref int index, int[] indexes) {
        if (indexes.Length > 0) {

            if (index + indexes.Length > Count) {
                index = Count - 1;

                if (index < 0) {
                    return false;
                }
            }

            var copy = GetFiles(indexes);
            var list = new List<IPackageFile>(copy.Length);

            list.AddRange(copy);

            Remove(indexes);

            _files.InsertRange(index, list);

            return true;
        }

        return false;
    }

    public bool MoveUp(int[] indexes) {
        if (indexes.Length > 0) {
            if (indexes[0] == 0) {
                return false;
            }

            var index = indexes[0] - 1;

            return MoveTo(ref index, indexes);
        }

        return false;
    }

    public void Remove(int[] indexes) {
        var length = indexes.Length;

        if (length > 0) {
            var array = GetFiles(indexes);

            for (var n = 0; n < length; n++) {
                _files.Remove(array[n]);
            }
        }
    }

    public List<IPackageFile> ToList() {
        return _files;
    }

    private IPackageFile[] GetFiles(int[] indexes) {
        var length = indexes.Length;
        var array = new IPackageFile[indexes.Length];

        for (var i = 0; i < length; i++) {
            array[i] = _files[indexes[i]];
        }

        return array;
    }
}