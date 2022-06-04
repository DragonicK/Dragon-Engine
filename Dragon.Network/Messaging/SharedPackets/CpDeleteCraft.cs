namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpDeleteCraft : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DeleteCraft;
}