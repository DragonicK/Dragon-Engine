using Dragon.Core.Model.Entity;
using Dragon.Game.Services;
using Dragon.Game.Configurations;
using Dragon.Game.Repository;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Combat.Death;

public class PlayerDeath : IEntityDeath {
    public IPacketSender? PacketSender { get; init; }
    public ContentService? ContentService { get; init; }
    public IConfiguration? Configuration { get; init; }
    public InstanceService? InstanceService { get; init; }
    public IPlayerRepository? PlayerRepository { get; init; }

    private ExperienceHandler? expHandler;

    public void Execute(IEntity? attacker, IEntity receiver) {
        if (expHandler is null) {
            expHandler = new ExperienceHandler() {
                Configuration = Configuration,
                ContentService = ContentService
            };
        }

        // remove experience
        // clear target
        // clear npc target
        // disable aura
        // remove effects -> remove icons on client
        // show dead panel
        // update attributes
        // send player corpse
    }
}