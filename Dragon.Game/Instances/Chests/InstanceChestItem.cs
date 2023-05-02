using Dragon.Core.Model;

namespace Dragon.Game.Instances.Chests;

public sealed class InstanceChestItem : IInstanceChestItem {
    public int Id { get; set; }
    public int Level { get; set; }
    public int Value { get; set; }
    public bool Bound { get; set; }
    public int UpgradeId { get; set; }
    public int AttributeId { get; set; }
    public bool IsCurrency { get; set; }
    public int WinnerTimeLimit { get; set; }
    public CurrencyType Currency { get; set; }
    public long CharacterIdFromRollDiceWinner { get; set; }
}