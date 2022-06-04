namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpWarehouseOpen : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.WarehouseOpen;
}