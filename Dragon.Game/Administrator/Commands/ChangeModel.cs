using Dragon.Core.Model;
using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Services;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeModel : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.SetModel;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ContentService? ContentService { get; set; }

    private const int MaximumParameters = 2;

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

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                Set(target, id);
            }
        }
    }

    private void Set(IPlayer? player, int id) {
        if (player is not null) {
            if (id > 0) {
                player!.Character.Model = id;

                var instanceId = player!.Character.Model;
                var instances = InstanceService!.Instances;

                if (instances.ContainsKey(instanceId)) {
                    PacketSender!.SendPlayerModel(player, instances[instanceId]);
                }
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }
}