namespace Dragon.Game.Instances.Chests;

public interface IInstanceChest {
    int Index { get; set; }
    int X { get; set; }
    int Y { get; set; }
    int ChestId { get; set; }
    int OpenedByCharacterId { get; set; }
    int KilledByCharacterId { get; set; }
    int PartyId { get; set; }
    int RemainingTime { get; set; }
    IInstance Instance { get; set; }
    IList<IInstanceChestItem> Items { get; set; }
}