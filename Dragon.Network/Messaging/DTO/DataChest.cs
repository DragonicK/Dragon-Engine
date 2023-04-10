using Dragon.Core.Model.Chests;

namespace Dragon.Network.Messaging.DTO;

public struct DataChest {
    public int Index { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Sprite { get; set; }
    public ChestState State { get; set; }
}