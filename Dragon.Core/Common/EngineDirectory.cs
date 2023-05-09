namespace Dragon.Core.Common;

public sealed class EngineDirectory {
    private readonly List<string> directory;

    public EngineDirectory() {
        directory = new List<string>();
    }

    public void Add(string folder) {
        directory.Add(folder);
    }

    public void Create() {
        directory.ForEach(path => {
            Directory.CreateDirectory(path);
        });
    }
}