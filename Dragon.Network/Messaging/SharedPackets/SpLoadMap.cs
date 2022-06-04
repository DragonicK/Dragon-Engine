namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpLoadMap : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.LoadMap;
    public int MapId { get; set; }
    public byte[] Key { get; set; } = Array.Empty<byte>();
    public byte[] Iv { get; set; } = Array.Empty<byte>();
}