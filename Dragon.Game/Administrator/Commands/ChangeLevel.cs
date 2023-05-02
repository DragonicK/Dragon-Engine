using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class ChangeLevel : IAdministratorCommand {
    public AdministratorCommands Command => AdministratorCommands.SetLevel;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 2;

    public ChangeLevel(IServiceInjector injector) {
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
            _ = int.TryParse(parameters[1], out var level);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Set(target, level);
        }
    }

    private void Set(IPlayer? player, int level) {
        var sender = GetPacketSender();

        if (player is not null) {
            if (level >= 1) {
                var maximum = Configuration!.Player.MaximumLevel;

                player.Character.Level = level > maximum ? maximum : level;

                player.AllocateAttributes();

                sender.SendAttributes(player);

                SendExperience(player);

                var instanceId = player!.Character.Map;
                var instances = InstanceService!.Instances;

                instances.TryGetValue(instanceId, out var instance);

                if (instance is not null) {
                    sender.SendPlayerVital(player, instance);
                    sender.SendPlayerDataTo(player, instance);
                }
            }
        }
    }

    private void SendExperience(IPlayer player) {
        var sender = GetPacketSender();

        var level = player!.Character.Level;
        var experience = ContentService!.PlayerExperience;

        var maximum = 0;

        if (level > 0) {
            if (level <= experience.MaximumLevel) {
                maximum = experience.Get(level);
            }
        }

        var minimum = player!.Character.Experience;

        if (minimum >= maximum) {
            minimum = maximum;

            player.Character.Experience = minimum;
        }

        sender.SendExperience(player, minimum, maximum);
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}