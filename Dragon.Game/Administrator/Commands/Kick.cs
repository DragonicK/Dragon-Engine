using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class Kick : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.Kick;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 1;

    public Kick(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                if (administrator.AccountLevel >= AccountLevel.Monitor) {

                    var repository = ConnectionService!.PlayerRepository;
                    var player = repository!.FindByName(parameters[0].Trim());

                    Disconnect(administrator, player);
                }
            }
        }
    }

    private void Disconnect(IPlayer administrator, IPlayer? player) {
        var sender = GetPacketSender();

        if (player is not null) {
            if (player.AccountLevel < AccountLevel.Monitor) {
                sender.SendAlertMessage(player, AlertMessageType.Kicked, MenuResetType.Login, true, true);
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