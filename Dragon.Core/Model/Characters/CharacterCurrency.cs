namespace Dragon.Core.Model.Characters;

public class CharacterCurrency {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public CurrencyType CurrencyId { get; set; }
    public int CurrencyValue { get; set; }
}