using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class WarpToLocation : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.WarpToLocation;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 2;

    public WarpToLocation(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                _ = int.TryParse(parameters[0], out var x);
                _ = int.TryParse(parameters[1], out var y);

                if (administrator.AccountLevel >= AccountLevel.Monitor) {
                    WarpLocation(administrator, x, y);
                }
            }
        }
    }

    private void WarpLocation(IPlayer administrator, int x, int y) {
        var sender = GetPacketSender();

        if (administrator.AccountLevel >= AccountLevel.GameMaster) {
            var instanceId = administrator.Character.Map;
            var instances = InstanceService!.Instances;

            instances.TryGetValue(instanceId, out var instance);

            if (instance is not null) {
                administrator.Character.X = x < 0 ? 0 : x;
                administrator.Character.Y = y < 0 ? 0 : y;
                administrator.Character.X = x > instance.MaximumX ? instance.MaximumX : x;
                administrator.Character.Y = y > instance.MaximumY ? instance.MaximumY : y;

                sender.SendPlayerXY(administrator, instance);
            }
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}