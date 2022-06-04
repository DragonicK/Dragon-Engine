using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat.Formulas;

public interface IMathResistances {
    bool CanResistSilence(IEntity attacker, IEntity receiver);
    bool CanResistBlind(IEntity attacker, IEntity receiver);
    bool CanResistStun(IEntity attacker, IEntity receiver);
}