using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat.Formulas {
    public class MathMagical : IMathMagical {
        private const int MaximumResistChance = 100;

        private readonly Random r;
        
        public MathMagical() {
            r = new Random();
        }

        public bool CanResistAttack(IEntity attacker, IEntity receiver) {
            var accuracy = attacker.Attributes.Get(SecondaryAttribute.MagicAccuracy);
            var evasion = receiver.Attributes.Get(SecondaryAttribute.MagicResist);

            var value = accuracy - evasion;

            var chance = 100 - (value / accuracy) * 100;

            if (chance > MaximumResistChance) {
                chance = MaximumResistChance;
            }

            if (chance < 0) {
                chance = 0;
            }

            return r.Next(1, 100) <= chance;
        }
    }
}