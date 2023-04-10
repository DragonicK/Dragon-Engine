namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpEnableChestTakeItem : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.EnableChestTakeItem;
}