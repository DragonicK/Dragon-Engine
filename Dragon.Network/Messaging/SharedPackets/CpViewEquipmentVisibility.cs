namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpViewEquipmentVisibility : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ViewEquipmentVisibility;
    public bool IsVisible { get; set; }
}