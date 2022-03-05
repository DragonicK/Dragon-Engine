using Crystalshire.Core.Logs;
using Crystalshire.Core.Services;
using Crystalshire.Core.Serialization;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.BlackMarket;

using Crystalshire.Game.Deaths;
using Crystalshire.Game.Combat.Formulas;

namespace Crystalshire.Game.Services {
    public class CombatService : IService, IUpdatableService {
        public ServicePriority Priority => ServicePriority.Last;
        public ConnectionService? ConnectionService { get; private set; }

        private IMathDodge? dodge;
        private IMathCritical? critical;
        private IMathMagical? magical;
        //private IParry? parry;
        //private IBlock? block;
        //private IResist resist;

        private IEntityDeath? playerDeath;
        private IEntityDeath? entityDeath;

        public void Start() {

        }

        public void Stop() {

        }

        public void Update(int detalTime) {
     
        }

 
    }
}