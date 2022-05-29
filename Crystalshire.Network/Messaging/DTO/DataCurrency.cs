using Crystalshire.Core.Model;

namespace Crystalshire.Network.Messaging.DTO {
    public struct DataCurrency {
        public CurrencyType Id { get; set; }
        public int Amount { get; set; }
    }
}