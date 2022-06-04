namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerMp : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerMp;
    public int Index { get; set; }
    public int MaximumMp { get; set; }
    public int Mp { get; set; }
}