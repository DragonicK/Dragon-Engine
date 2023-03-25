namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpClearCast : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ClearCast;
}