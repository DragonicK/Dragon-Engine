namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerHp : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerHp;
    public int Index { get; set; }
    public int MaximumHp { get; set; }
    public int Hp { get; set; }
}