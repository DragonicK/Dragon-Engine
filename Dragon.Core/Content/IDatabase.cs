namespace Dragon.Core.Content;

public interface IDatabase<T> {
    string Folder { get; set; }
    string FileName { get; set; }
    int Count { get; }
    T? this[int id] { get; set; }
    void Add(int id, T item);
    bool Contains(int id);
    void Remove(int id);
    void Clear();
    virtual void Load() { }
    virtual void Save() { }
    IEnumerator<KeyValuePair<int, T>> GetEnumerator();
    string[]? GetFolders(string root);
    string[]? GetFiles(string folder);
}