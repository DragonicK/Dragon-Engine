using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpCurrencyUpdate : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.CurrencyUpdate;
        public DataCurrency Currency { get; set; }
    }
}