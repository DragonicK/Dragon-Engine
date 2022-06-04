using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpInstanceEntityDirection : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.InstanceEntityDirection;
    public int Index { get; set; }
    public Direction Direction { get; set; }
}