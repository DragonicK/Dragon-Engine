using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat.Formulas;

public class MathResistances : IMathResistances {
    public bool CanResistBlind(IEntity attacker, IEntity receiver) {
        return false;
    }

    public bool CanResistSilence(IEntity attacker, IEntity receiver) {
        return false;
    }

    public bool CanResistStun(IEntity attacker, IEntity receiver) {
        return false;
    }
}