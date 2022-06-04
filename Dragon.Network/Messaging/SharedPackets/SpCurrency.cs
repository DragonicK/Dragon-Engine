using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCurrency : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.Currency;
    public DataCurrency[] Currencies { get; set; } = Array.Empty<DataCurrency>();
}