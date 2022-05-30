using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat.Formulas;

public class MathCritical : IMathCritical {
    private readonly Random r;

    public MathCritical() {
        r = new Random();
    }

    public bool CanCriticalAttack(IEntity attacker, IEntity receiver) {
        var rate = attacker.Attributes.Get(UniqueAttribute.CritRate);
        var resist = receiver.Attributes.Get(UniqueAttribute.ResistCritRate);

        var chance = rate - resist;

        if (chance < 0) {
            return false;
        }

        return r.Next(0, 100) <= Convert.ToInt32(chance * 100);
    }
}