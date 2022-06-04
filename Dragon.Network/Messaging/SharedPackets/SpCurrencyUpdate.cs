using Dragon.Network.Messaging.DTO;

namespace Dragon.Network.Messaging.SharedPackets;

public sealed class SpCurrencyUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CurrencyUpdate;
    public DataCurrency Currency { get; set; }
}