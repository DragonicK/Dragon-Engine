namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpEquipHeraldry : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.EquipHeraldry;
    public int HeraldryIndex { get; set; }
    public int InventoryIndex { get; set; }
}