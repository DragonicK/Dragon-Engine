using Crystalshire.Core.Model.Skills;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Combat;

public interface ISkill {
    IList<Target> GetTarget(Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect);
    bool CouldSelect(Target target, SkillEffect effect);
    Damaged GetDamage(Target target, CharacterSkill inventory, SkillEffectType type);
    void Inflict(Damaged damaged, Target target, IInstance instance, SkillEffect effect);
}