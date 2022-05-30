namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class CpStopCraft : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.StopCraft;
}