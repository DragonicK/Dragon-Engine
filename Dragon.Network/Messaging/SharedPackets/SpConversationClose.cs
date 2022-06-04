namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpConversationClose : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ConversationClose;
}