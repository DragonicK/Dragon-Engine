using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Manager;
using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class WarpToMe : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.WarpToMe;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 1;

    private readonly WarperManager WarperManager;

    public WarpToMe(IServiceInjector injector) {
        injector.Inject(this);

        WarperManager = new WarperManager(injector);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                if (administrator.AccountLevel >= AccountLevel.Monitor) {
                    var repository = ConnectionService!.PlayerRepository;
                    var target = repository!.FindByName(parameters[0].Trim());

                    MoveToAdministrator(administrator, target);
                }
            }
        }
    }

    private void MoveToAdministrator(IPlayer administrator, IPlayer? target) {
        var sender = GetPacketSender();

        if (target is not null) {
            var instances = InstanceService!.Instances;

            var instanceId = administrator!.Character.Map;
            var x = administrator!.Character.X;
            var y = administrator!.Character.Y;

            instances.TryGetValue(instanceId, out var instance);

            if (instance is not null) {
                target.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
                target.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

                WarperManager.Warp(target, instance, target.Character.X, target.Character.Y);
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