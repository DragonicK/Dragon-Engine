using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat.Formulas;

public interface IMathDodge {
    bool CanDodgeAttack(IEntity attacker, IEntity receiver);
}