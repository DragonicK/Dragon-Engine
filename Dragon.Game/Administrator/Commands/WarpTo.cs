using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;

namespace Dragon.Game.Administrator.Commands;

public sealed class WarpTo : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.WarpTo;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 1;

    private readonly WarperManager WarperManager;

    public WarpTo(IServiceInjector injector) {
        injector.Inject(this);

        WarperManager = new WarperManager(injector);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                _ = int.TryParse(parameters[0], out var instanceId);

                if (administrator.AccountLevel >= AccountLevel.Monitor) {
                    var instances = InstanceService!.Instances;

                    if (instances.ContainsKey(instanceId)) {
                        MoveTo(administrator, instances[instanceId]);
                    }
                }
            }
        }
    }

    private void MoveTo(IPlayer administrator, IInstance instance) {
        var x = administrator.Character.X;
        var y = administrator.Character.Y;

        administrator.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
        administrator.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

        WarperManager.Warp(administrator, instance, administrator.Character.X, administrator.Character.Y);
    }
}