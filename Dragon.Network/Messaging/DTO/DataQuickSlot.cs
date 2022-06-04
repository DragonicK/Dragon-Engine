using Dragon.Core.Model;

namespace Dragon.Network.Messaging.DTO;

public struct DataQuickSlot {
    public int Index { get; set; }
    public QuickSlotType ObjectType { get; set; }
    public int ObjectValue { get; set; }
}