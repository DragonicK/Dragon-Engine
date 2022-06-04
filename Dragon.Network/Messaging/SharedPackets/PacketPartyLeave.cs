namespace Dragon.Network.Messaging.SharedPackets;

public sealed class PacketPartyLeave : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.PartyLeave;
}