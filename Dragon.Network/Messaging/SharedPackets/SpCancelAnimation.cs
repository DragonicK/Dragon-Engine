namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCancelAnimation : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CancelAnimation;
    public int Index { get; set; }
}