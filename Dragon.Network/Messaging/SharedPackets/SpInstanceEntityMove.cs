using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpInstanceEntityMove : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.InstanceEntityMove;
    public int Index { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; }
    public MovementType MovementType { get; set; }
}