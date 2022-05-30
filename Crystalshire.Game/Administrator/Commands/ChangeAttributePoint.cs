using Crystalshire.Core.Model;
using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Administrator.Commands;

public sealed class ChangeAttributePoint : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.SetAttributePoint;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ContentService? ContentService { get; set; }

    private const int MaximumParameters = 3;

    public void Process(string[]? parameters) {
        if (parameters is not null) {
            if (parameters.Length >= MaximumParameters) {
                ContinueProcess(parameters);
            }
        }
    }

    private void ContinueProcess(string[] parameters) {
        if (Administrator is not null) {
            if (Administrator.AccountLevel >= AccountLevel.Superior) {
                int.TryParse(parameters[1], out var value);
                bool.TryParse(parameters[2], out var shouldAdd);

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                Set(target, value, shouldAdd);
            }
        }
    }

    private void Set(IPlayer? player, long value, bool shouldAdd) {
        if (player is not null) {
            if (value >= 0 && value <= int.MaxValue) {

                if (shouldAdd) {
                    var p = player.Character.Points;
                    player.Character.Points += CanSum(p, value) ? (int)value : 0;
                }
                else {
                    player.Character.Points = (int)value;
                }

                PacketSender!.SendAttributePoint(player);
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }

    private bool CanSum(long a, long b) {
        return (a + b) <= int.MaxValue;
    }
}