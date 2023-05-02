using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class WarpMeTo : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.WarpMeTo;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 1;

    private readonly WarperManager WarperManager;

    public WarpMeTo(IServiceInjector injector) {
        injector.Inject(this);

        WarperManager = new WarperManager(injector);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                if (administrator.AccountLevel >= AccountLevel.Monitor) {
                    var repository = ConnectionService!.PlayerRepository;
                    var destination = repository!.FindByName(parameters[0].Trim());

                    MoveToDestination(administrator, destination);
                }
            }
        }
    }

    private void MoveToDestination(IPlayer administrator, IPlayer? destination) {
        var sender = GetPacketSender();

        if (destination is not null) {
            var instances = InstanceService!.Instances;

            var instanceId = destination.Character.Map;
            var x = destination.Character.X;
            var y = destination.Character.Y;

            instances.TryGetValue(instanceId, out var instance);

            if (instance is not null) {
                administrator.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
                administrator.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

                WarperManager.Warp(administrator, instance, administrator.Character.X, administrator.Character.Y);
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}