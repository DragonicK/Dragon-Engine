using Crystalshire.Network.Messaging.DTO;

namespace Crystalshire.Network.Messaging.SharedPackets;

public sealed class SpCurrencyUpdate : IMessagePacket {
    public MessageHeader Header { get; set; } = MessageHeader.CurrencyUpdate;
    public DataCurrency Currency { get; set; }
}