using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeAttributePoint : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.SetAttributePoint;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 3;

    public ChangeAttributePoint(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Process(IPlayer administrator, string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                ContinueProcess(administrator, parameters);
            }
        }
    }

    private void ContinueProcess(IPlayer administrator, string[] parameters) {
        if (administrator.AccountLevel >= AccountLevel.Superior) {
            _ = int.TryParse(parameters[1], out var value);
            _ = bool.TryParse(parameters[2], out var shouldAdd);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Set(administrator, target, value, shouldAdd);
        }
    }

    private void Set(IPlayer administrator, IPlayer? player, long value, bool shouldAdd) {
        var sender = GetPacketSender();

        if (player is not null) {
            if (value >= 0 && value <= int.MaxValue) {
                if (shouldAdd) {
                    var p = player.Character.Points;
                    player.Character.Points += CanSum(p, value) ? (int)value : 0;
                }
                else {
                    player.Character.Points = (int)value;
                }

                sender.SendAttributePoint(player);
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private bool CanSum(long a, long b) {
        return (a + b) <= int.MaxValue;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}