namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpWarehouseClose : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.WarehouseClose;
}