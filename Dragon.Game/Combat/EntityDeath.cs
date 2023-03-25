using Dragon.Core.Model.Entity;
using Dragon.Game.Instances;
using Dragon.Game.Players;

namespace Dragon.Game.Combat;
public class EntityDeath : IEntityDeath {
    public void Execute(IEntity? attacker, IEntity receiver) {
        if (attacker is IPlayer) {

        }
        else if (attacker is IInstanceEntity) { 

        }
    }

    private void ExecutePlayerDeath() {

    }

    private void ExecuteEntityDeath() {

    }

}