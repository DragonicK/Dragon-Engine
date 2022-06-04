namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpGettingMap : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.GettingMap;
    public bool IsGettingMap { get; set; }
}