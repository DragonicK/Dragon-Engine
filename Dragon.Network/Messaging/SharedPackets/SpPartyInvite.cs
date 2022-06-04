namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpPartyInvite : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PartyInvite;
    public string FromName { get; set; } = string.Empty;
}