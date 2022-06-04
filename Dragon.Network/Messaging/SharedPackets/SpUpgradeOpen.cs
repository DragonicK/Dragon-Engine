namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpUpgradeOpen : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UpgradeOpen;
}