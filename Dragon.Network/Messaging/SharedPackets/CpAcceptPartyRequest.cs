namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpAcceptPartyRequest : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.AcceptParty;
}