using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat.Formulas;

public interface IMathCritical {
    bool CanCriticalAttack(IEntity attacker, IEntity receiver);
}