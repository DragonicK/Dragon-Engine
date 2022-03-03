using Crystalshire.Core.Network.Messaging.DTO;

namespace Crystalshire.Core.Network.Messaging.SharedPackets {
    public sealed class SpCurrency : IMessagePacket {
        public MessageHeader Header { get; set; } = MessageHeader.Currency;
        public DataCurrency[] Currencies { get; set; } = Array.Empty<DataCurrency>();
    }
}