using Dragon.Core.Logs;
using Dragon.Core.Services;
using Dragon.Core.Serialization;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.BlackMarket;

using Dragon.Game.Deaths;
using Dragon.Game.Combat.Formulas;

namespace Dragon.Game.Services;

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