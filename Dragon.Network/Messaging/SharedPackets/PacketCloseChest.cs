namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketCloseChest : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CloseChest;
    public int Index { get; set; }
}