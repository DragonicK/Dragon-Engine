using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeClass : IAdministratorCommand {
    public AdministratorCommands Command => AdministratorCommands.SetClass;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 2;

    public ChangeClass(IServiceInjector injector) {
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
            _ = short.TryParse(parameters[1], out var id);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Set(administrator, target, id);
        }
    }

    private void Set(IPlayer administrator, IPlayer? player, short id) {
        var sender = GetPacketSender();
        var classes = ContentService!.Classes;

        if (player is not null) {
            if (classes.Contains(id)) {
                player.Class = classes[id]!;
                player.Character.ClassCode = id;

                player.AllocateAttributes();

                sender.SendAttributes(player);

                var instance = GetInstance(player);

                if (instance is not null) {
                    sender.SendPlayerDataTo(player, instance);
                    sender.SendPlayerVital(player, instance);
                }
                else {
                    sender.SendPlayerVital(player);
                }
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player.Character.Map;
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}