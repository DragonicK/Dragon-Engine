namespace Dragon.Core.Model.Chests;

public class Chest {
    public int Id { get; set; }

    public int Count {
        get => Items.Count;
    }

    public IList<ChestItem> Items { get; set; }

    public ChestItem this[int index] {
        get => Items[index];
    }

    public Chest() {
        Items = new List<ChestItem>();
    }
}