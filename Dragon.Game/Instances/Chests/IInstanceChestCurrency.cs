using Dragon.Core.Model;

namespace Dragon.Game.Instances.Chests;

public interface IInstanceChestCurrency {
    public CurrencyType Currency { get; set; }
    public int Value { get; set; }
}