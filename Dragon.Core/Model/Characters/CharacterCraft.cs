using Dragon.Core.Model.Crafts;

namespace Dragon.Core.Model.Characters;

public class CharacterCraft {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public CraftType Type { get; set; }
    public int Level { get; set; }
    public int Experience { get; set; }
}