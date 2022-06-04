namespace Dragon.Core.Model.Characters;

public class CharacterVital {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int Health { get; set; }
    public int Mana { get; set; }
    public int Special { get; set; }
}