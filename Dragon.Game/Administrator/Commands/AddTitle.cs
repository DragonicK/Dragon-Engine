using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;
using Dragon.Game.Instances;

namespace Dragon.Game.Administrator.Commands;

public sealed class AddTitle : IAdministratorCommand {
    public AdministratorCommands Command { get; } = AdministratorCommands.AddTitle;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 2;

    public AddTitle(IServiceInjector injector) {
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
            _ = int.TryParse(parameters[1], out var id);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Add(administrator, target, id);
        }
    }

    private void Add(IPlayer administrator, IPlayer? player, int id) {
        var sender = GetPacketSender();

        var titles = ContentService!.Titles;

        if (player is not null) {
            if (titles.Contains(id)) {
                var result = player!.Titles.Add(id);

                if (result) {
                    player.Titles.UpdateAttributes();

                    player.AllocateAttributes();

                    sender.SendAttributes(player);

                    var instances = GetInstances();
                    var instanceId = player.Character.Map;

                    instances.TryGetValue(instanceId, out var instance);

                    if (instance is not null) {
                        sender.SendPlayerVital(player, instance);
                    }

                    sender.SendTitles(player);
                }
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private IDictionary<int, IInstance> GetInstances() {
        return InstanceService!.Instances!;
    }


    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}