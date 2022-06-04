using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpServerRates : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.ServerRates;
    public DataRate Rates { get; set; }
}