using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat.Formulas;

public class MathDodge : IMathDodge {

    private const int MaximumDodgeChance = 90;

    private readonly Random r;

    public MathDodge() {
        r = new Random();
    }

    public bool CanDodgeAttack(IEntity attacker, IEntity receiver) {
        var accuracy = attacker.Attributes.Get(SecondaryAttribute.Accuracy);
        var evasion = receiver.Attributes.Get(SecondaryAttribute.Evasion);

        var value = accuracy - evasion;

        var chance = 100 - (value / accuracy) * 100;

        if (chance > MaximumDodgeChance) {
            chance = MaximumDodgeChance;
        }

        if (chance < 0) {
            chance = 0;
        }

        return r.Next(1, 100) <= chance;
    }
}
