namespace Dragon.Network.Messaging.SharedPackets;

public sealed class CpPartyKick : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PartyKick;
    public int MemberIndex { get; set; }
}