namespace Crystalshire.Core.Model.Gashas;

public class Gasha {
    public int Id { get; set; }

    public int Count {
        get => Items.Count;
    }

    public IList<GashaItem> Items { get; set; }

    public GashaItem this[int index] {
        get => Items[index];
    }

    public Gasha() {
        Items = new List<GashaItem>();
    }
}