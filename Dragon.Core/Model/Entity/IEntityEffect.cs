using Dragon.Core.Content;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Characters;

namespace Dragon.Core.Model.Entity;

public interface IEntityEffect {
    IEntityAttribute Attributes { get; }
    IDatabase<Effect>? Effects { get; set; }
    IDatabase<GroupAttribute>? EffectAttributes { get; set; }
    IDatabase<GroupAttribute>? EffectUpgrades { get; set; }
    int Count { get; }
    CharacterAttributeEffect Add(int id, int level, int duration, bool isAura = false);
    bool Contains(int id);
    void Remove(int id);
    void UpdateAttributes();
    CharacterAttributeEffect? GetOverridable(Effect source);
    CharacterAttributeEffect? GetEffectById(int id);
    IList<CharacterAttributeEffect> ToList();
}