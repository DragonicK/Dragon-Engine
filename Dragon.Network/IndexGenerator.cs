namespace Dragon.Network;

public sealed class IndexGenerator : IIndexGenerator {
    private readonly HashSet<int> indexes;
    private readonly int Maximum;

    public IndexGenerator(int maximum) {
        indexes = new HashSet<int>(maximum);
        Maximum = maximum;
    }

    public int GetNextIndex() {
        for (var i = 1; i <= Maximum; ++i) {
            if (!indexes.Contains(i)) {
                indexes.Add(i);

                return i;
            }
        }

        return 0;
    }

    public void Remove(int index) {
        indexes.Remove(index);
    }
}