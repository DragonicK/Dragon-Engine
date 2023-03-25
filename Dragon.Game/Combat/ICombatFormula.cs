using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat;

public interface ICombatFormula {
    bool CanDodgeAttack(IEntity attacker, IEntity receiver);
    bool CanCriticalAttack(IEntity attacker, IEntity receiver);
    bool CanResistAttack(IEntity attacker, IEntity receiver);
    bool CanResistSilence(IEntity attacker, IEntity receiver);
    bool CanResistBlind(IEntity attacker, IEntity receiver);
    bool CanResistStun(IEntity attacker, IEntity receiver);
}