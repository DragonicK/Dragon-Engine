using Dragon.Core.Model;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpAnimation : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Animation;
    public int Animation { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public TargetType LockType { get; set; }
    public int LockIndex { get; set; }
    public bool IsCasting { get; set; }
}