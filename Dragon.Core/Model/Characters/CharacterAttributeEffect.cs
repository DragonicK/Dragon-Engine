using System.ComponentModel.DataAnnotations.Schema;

namespace Dragon.Core.Model.Characters;

public class CharacterAttributeEffect {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int EffectId { get; set; }
    public int EffectLevel { get; set; }
    public int EffectDuration { get; set; }

    [NotMapped]
    public bool IsAura { get; set; }
}