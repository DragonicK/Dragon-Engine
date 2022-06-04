using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPlayerXY : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerXY;
    public int Index { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
}