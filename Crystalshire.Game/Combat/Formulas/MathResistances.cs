using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Combat.Formulas {
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
}