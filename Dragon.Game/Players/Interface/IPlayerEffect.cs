using Dragon.Core.Content;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

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