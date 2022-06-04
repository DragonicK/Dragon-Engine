using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat.Formulas;

public interface IMathDodge {
    bool CanDodgeAttack(IEntity attacker, IEntity receiver);
}