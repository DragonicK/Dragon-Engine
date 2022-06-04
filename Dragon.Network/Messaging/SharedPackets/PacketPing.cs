namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketPing : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Ping;
    public bool ClienteRequest { get; set; } = false;
}