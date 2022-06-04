namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketCast : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Cast;
    public int Index { get; set; }
}