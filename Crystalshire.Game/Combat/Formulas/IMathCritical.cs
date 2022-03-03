using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat.Formulas {
    public interface IMathCritical {
        bool CanCriticalAttack(IEntity attacker, IEntity receiver);
    }
}