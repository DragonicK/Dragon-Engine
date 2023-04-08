using Dragon.Core.Model.Chests;

namespace Dragon.Game.Instances.Chests;

public interface IInstanceChest {
    int Index { get; set; }
    int X { get; set; }
    int Y { get; set; }
    Chest Chest { get; set; }
    long OpenedByCharacterId { get; set; }
    long KilledByCharacterId { get; set; }
    int PartyId { get; set; }
    int RemainingTime { get; set; }
    IInstance Instance { get; set; }
    IList<IInstanceChestItem> Items { get; set; }
}