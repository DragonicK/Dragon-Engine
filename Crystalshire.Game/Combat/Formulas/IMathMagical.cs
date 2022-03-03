using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat.Formulas {
    public interface IMathMagical {
        bool CanResistAttack(IEntity attacker, IEntity receiver);
    }
}