using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketPlayerMovement : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PlayerMovement;
    public int Index { get; set; }
    public Direction Direction { get; set; }
    public MovementType State { get; set; }
    public short X { get; set; }
    public short Y { get; set; }
}