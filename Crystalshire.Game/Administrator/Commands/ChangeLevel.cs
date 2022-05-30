using Crystalshire.Core.Model;
using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Administrator.Commands;

public sealed class ChangeLevel : IAdministratorCommand {
    public AdministratorCommands Command => AdministratorCommands.SetLevel;
    public IPlayer? Administrator { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public InstanceService? InstanceService { get; set; }
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
                int.TryParse(parameters[1], out var level);

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                Set(target, level);
            }
        }
    }

    private void Set(IPlayer? player, int level) {
        if (player is not null) {
            if (level >= 1) {

                var maximum = Configuration!.Player.MaximumLevel;

                player.Character.Level = level > maximum ? maximum : level;

                player.AllocateAttributes();

                PacketSender!.SendAttributes(player);

                SendExperience(player);

                var instanceId = player!.Character.Map;
                var instances = InstanceService!.Instances;

                if (instances.ContainsKey(instanceId)) {
                    PacketSender!.SendPlayerVital(player, instances[instanceId]);
                    PacketSender!.SendPlayerDataTo(player, instances[instanceId]);
                }
            }
        }
    }

    private void SendExperience(IPlayer player) {
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

        PacketSender!.SendExperience(player!, minimum, maximum);
    }
}