namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpRequestViewEquipment : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.RequestViewEquipment;
    public string Character { get; set; } = string.Empty;
}