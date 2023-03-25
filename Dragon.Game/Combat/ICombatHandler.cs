using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Characters;

using Dragon.Game.Instances;

namespace Dragon.Game.Combat;

public interface ICombatHandler {
    IList<Target> GetTarget(Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect);
    bool CanSelect(Target target, SkillEffect effect);
    Damaged GetDamage(Target target, CharacterSkill inventory, SkillEffectType type);
    void Inflict(Damaged damaged, Target target, IInstance instance, SkillEffect effect);
}