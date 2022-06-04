namespace Dragon.Network.Messaging.SharedPackets;


public sealed class SpClosePartyInvitation : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ClosePartyInvitation;
}