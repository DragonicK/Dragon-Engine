namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpUnequipHeraldry : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.UnequipHeraldry;
    public int Index { get; set; }
}