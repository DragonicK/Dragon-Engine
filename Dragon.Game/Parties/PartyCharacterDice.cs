namespace Dragon.Game.Parties;

public struct PartyCharacterDice {
    public long CharacterId { get; set; }
    public int RolledNumber { get; set; }
    public PartyCharacterDiceState State { get; set; }
}