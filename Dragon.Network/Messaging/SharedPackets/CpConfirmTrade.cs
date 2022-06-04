namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpConfirmTrade : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ConfirmTrade;
}