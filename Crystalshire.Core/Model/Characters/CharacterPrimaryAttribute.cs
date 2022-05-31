namespace Crystalshire.Core.Model.Characters;

public class CharacterPrimaryAttribute {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Constitution { get; set; }
    public int Intelligence { get; set; }
    public int Spirit { get; set; }
    public int Will { get; set; }
}