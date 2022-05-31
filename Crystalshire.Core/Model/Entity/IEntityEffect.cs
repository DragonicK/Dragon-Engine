using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Effects;
using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Core.Model.Entity;

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