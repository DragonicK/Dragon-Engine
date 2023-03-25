using Dragon.Core.Logs;
using Dragon.Core.Services;
using Dragon.Core.Serialization;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.BlackMarket;

using Dragon.Game.Deaths;
using Dragon.Game.Combat.Formulas;
using Dragon.Game.Players;

namespace Dragon.Game.Services;

public class CombatService : IService, IUpdatableService {
    public ServicePriority Priority => ServicePriority.Last;
    public ConnectionService? ConnectionService { get; private set; }

    public void Start() {

    }

    public void Stop() {

    }

    public void Update(int detalTime) {
        var players = ConnectionService!.PlayerRepository!.GetPlayers();

        foreach (var (_, player) in players) {
            if (player is not null) {
                if (player.InGame) {
                    if (player.Combat.IsBufferedSkill) {
                        ProcessCastSkill(player);
                    }
                }
            }
        }
    }

    private void ProcessCastSkill(IPlayer player) {
        var index = player.Combat.BufferedSkillIndex;
        var timer = player.Combat.BufferedSkillTime;

        if (Environment.TickCount >= timer) {
            player.Combat.CastSkill(index);

            player.Combat.IsBufferedSkill = false;
            player.Combat.BufferedSkillIndex = 0;
            player.Combat.BufferedSkillTime = 0;
        }
    }
}