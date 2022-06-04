namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketDeleteMail : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DeleteMail;
    public int[] Id { get; set; } = Array.Empty<int>();
}