using Dragon.Core.Model;

namespace Dragon.Network.Messaging.DTO;

public struct DataCurrency {
    public CurrencyType Id { get; set; }
    public int Amount { get; set; }
}