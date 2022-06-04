namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpConversation : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Conversation;
    public int NpcId { get; set; }
}