namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpPartyRequest : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PartyRequest;
    public string Character { get; set; } = string.Empty;
}