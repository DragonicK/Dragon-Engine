namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpCompletedCraft : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CompletedCraft;
}