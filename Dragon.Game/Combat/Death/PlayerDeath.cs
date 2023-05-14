using Dragon.Core.Services;
using Dragon.Core.Model.Entity;

using Dragon.Game.Manager;
using Dragon.Game.Services;

namespace Dragon.Game.Combat.Death;

public sealed class PlayerDeath : IEntityDeath {
    public LoggerService? LoggerService { get; private set; }
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly ExperienceManager ExpHandler;

    public PlayerDeath(IServiceInjector injector) {
        injector.Inject(this);

        ExpHandler = new ExperienceManager(injector);
    }

    public void Execute(IEntity? attacker, IEntity receiver) {
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