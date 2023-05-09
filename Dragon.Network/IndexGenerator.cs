namespace Dragon.Network;

public sealed class IndexGenerator : IIndexGenerator {
    private readonly HashSet<int> indexes;
    private readonly int Maximum;

    private readonly object _lock = new();

    public IndexGenerator(int maximum) {
        indexes = new HashSet<int>(maximum);
        Maximum = maximum;
    }

    public int GetNextIndex() {
        lock (_lock) {
            for (var i = 1; i <= Maximum; ++i) {
                if (!indexes.Contains(i)) {
                    indexes.Add(i);

                    return i;
                }
            }

            return 0;
        }
    }

    public void Remove(int index) {
        lock (_lock) {
            indexes.Remove(index);
        }
    }
}