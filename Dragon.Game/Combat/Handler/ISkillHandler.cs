using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Characters;

using Dragon.Game.Instances;
using Dragon.Game.Combat.Common;

namespace Dragon.Game.Combat.Handler;

public interface ISkillHandler {
    IList<Target> GetTarget(ref Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect);
    bool CanSelect(ref Target target, SkillEffect effect);
    Damaged GetDamage(ref Target target, CharacterSkill inventory, SkillEffectType type);
    void Inflict(ref Damaged damaged, ref Target target, IInstance instance, SkillEffect effect);
    void ResetTargets();
}