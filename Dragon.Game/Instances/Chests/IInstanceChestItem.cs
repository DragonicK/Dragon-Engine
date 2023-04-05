namespace Dragon.Game.Instances.Chests;

public interface IInstanceChestItem : IInstanceChestCurrency {
    public int Id { get; set; }
    public int Level { get; set; }
    public bool Bound { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public bool IsCurrency { get; set; }
    public int CharacterIdFromRollDiceWinner { get; set; }
    public int WinnerTimeLimit { get; set; }
}