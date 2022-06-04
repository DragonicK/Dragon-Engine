namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketConversationOption : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ConversationOption;
    public int ConversationId { get; set; }
    public int ChatIndex { get; set; }
    public int Option { get; set; }
}