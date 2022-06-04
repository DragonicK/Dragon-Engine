namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCraftClear : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CraftClear;
}