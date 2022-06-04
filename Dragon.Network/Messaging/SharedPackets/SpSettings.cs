namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpSettings : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Settings;
    public bool ViewEquipment { get; set; }
}