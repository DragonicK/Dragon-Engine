using Dragon.Core.Model;

using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Players;

namespace Dragon.Game.Administrator.Commands;

public sealed class Kick : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.Kick;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ContentService? ContentService { get; set; }

    private const int MaximumParameters = 1;

    public void Process(string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                if (Administrator is not null) {
                    if (Administrator.AccountLevel >= AccountLevel.Monitor) {

                        var repository = ConnectionService!.PlayerRepository;
                        var player = repository!.FindByName(parameters[0].Trim());

                        Disconnect(player);
                    }
                }
            }
        }
    }

    private void Disconnect(IPlayer? player) {
        if (player is not null) {
            if (player.AccountLevel < AccountLevel.Monitor) {
                PacketSender!.SendAlertMessage(player, AlertMessageType.Kicked, MenuResetType.Login, true, true);
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }
}