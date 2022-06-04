namespace Dragon.Network.Messaging.SharedPackets;

public class PacketSelectedTitle : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.SelectedTitle;
    public int Index { get; set; }
    public int Id { get; set; }
}