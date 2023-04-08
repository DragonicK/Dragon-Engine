using System.Text.Json.Serialization;

namespace Dragon.Core.Model.Chests;

public class Chest {
    public int Id { get; set; }
    public int Sprite { get; set; }
    public int MaximumDisplayedItems { get; set; } = 20;

    [JsonIgnore]
    public int Count {
        get => Items.Count;
    }

    [JsonIgnore]
    public IList<ChestItem> Items { get; set; }

    public ChestItem this[int index] {
        get => Items[index];
    }

    public Chest() {
        Items = new List<ChestItem>();
    }
}