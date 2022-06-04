using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat.Formulas;

public interface IMathMagical {
    bool CanResistAttack(IEntity attacker, IEntity receiver);
}