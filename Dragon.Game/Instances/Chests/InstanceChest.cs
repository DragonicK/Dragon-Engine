namespace Dragon.Game.Instances.Chests;

public class InstanceChest : IInstanceChest {
    public int Index { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int ChestId { get; set; }
    public int OpenedByCharacterId { get; set; }
    public int KilledByCharacterId { get; set; }
    public int PartyId { get; set; }
    public int RemainingTime { get; set; }
    public IInstance Instance { get; set; }
    public IList<IInstanceChestItem> Items { get; set; }

    public InstanceChest(IInstance instance) {
        Instance = instance;

        Items = new List<IInstanceChestItem>();
    }
}