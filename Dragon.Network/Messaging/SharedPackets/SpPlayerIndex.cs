namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerIndex : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SetPlayerIndex;
    public int Index { get; set; }
}