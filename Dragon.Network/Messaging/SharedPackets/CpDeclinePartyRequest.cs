namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpDeclinePartyRequest : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.DeclineParty;
}