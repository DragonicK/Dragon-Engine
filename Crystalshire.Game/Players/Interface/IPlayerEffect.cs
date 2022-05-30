using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Effects;
using Crystalshire.Core.Model.Attributes;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players;

public interface IPlayerEffect {
    int Count { get; }
    IEntityAttribute Attributes { get; }
    IDatabase<Effect>? Effects { get; set; }
    IDatabase<GroupAttribute>? EffectAttributes { get; set; }
    IDatabase<GroupAttribute>? EffectUpgrades { get; set; }
    CharacterAttributeEffect Add(int id, int level, int duration, bool isAura = false);
    bool Contains(int id);
    void Remove(int id);
    void UpdateAttributes();
    IList<CharacterAttributeEffect> ToList();
    CharacterAttributeEffect? GetOverridable(Effect source);
}