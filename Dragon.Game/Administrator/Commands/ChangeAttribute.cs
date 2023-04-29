using Dragon.Core.Model;
using Dragon.Game.Network.Senders;
using Dragon.Game.Players;
using Dragon.Game.Services;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeAttribute : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.SetAttribute;
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
                int.TryParse(parameters[1], out var id);
                int.TryParse(parameters[2], out var value);

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                Set(target, GetAttribute(id), value);
            }
        }
    }

    private void Set(IPlayer? player, PrimaryAttribute id, int value) {
        if (player is not null) {
            player.PrimaryAttributes.Set(id, value);

            player.AllocateAttributes();

            PacketSender!.SendAttributes(player);
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }

    private PrimaryAttribute GetAttribute(int id) {
        if (Enum.IsDefined(typeof(PrimaryAttribute), id)) {
            return (PrimaryAttribute)id;
        }

        return PrimaryAttribute.Strength;
    }
}