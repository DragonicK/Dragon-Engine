using Dragon.Core.Model;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Combat.Formula;

public class CombatFormula : ICombatFormula {
    private const int MaximumResistChance = 100;
    private const int MaximumDodgeChance = 90;

    private readonly Random r;

    public CombatFormula() {
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

    public bool CanDodgeAttack(IEntity attacker, IEntity receiver) {
        var accuracy = attacker.Attributes.Get(SecondaryAttribute.Accuracy);
        var evasion = receiver.Attributes.Get(SecondaryAttribute.Evasion);

        var value = accuracy - evasion;

        var chance = 100 - value / accuracy * 100;

        if (chance > MaximumDodgeChance) {
            chance = MaximumDodgeChance;
        }

        if (chance < 0) {
            chance = 0;
        }

        return r.Next(1, 100) <= chance;
    }

    public bool CanResistAttack(IEntity attacker, IEntity receiver) {
        var accuracy = attacker.Attributes.Get(SecondaryAttribute.MagicAccuracy);
        var evasion = receiver.Attributes.Get(SecondaryAttribute.MagicResist);

        var value = accuracy - evasion;

        var chance = 100 - value / accuracy * 100;

        if (chance > MaximumResistChance) {
            chance = MaximumResistChance;
        }

        if (chance < 0) {
            chance = 0;
        }

        return r.Next(1, 100) <= chance;
    }

    public bool CanResistBlind(IEntity attacker, IEntity receiver) {
        throw new NotImplementedException();
    }

    public bool CanResistSilence(IEntity attacker, IEntity receiver) {
        throw new NotImplementedException();
    }

    public bool CanResistStun(IEntity attacker, IEntity receiver) {
        throw new NotImplementedException();
    }
}