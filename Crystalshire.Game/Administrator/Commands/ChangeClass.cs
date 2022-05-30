using Crystalshire.Core.Model;
using Crystalshire.Game.Network;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Administrator.Commands;

public sealed class ChangeClass : IAdministratorCommand {
    public AdministratorCommands Command => AdministratorCommands.SetClass;
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
                short.TryParse(parameters[1], out var id);

                var repository = ConnectionService!.PlayerRepository;
                var target = repository!.FindByName(parameters[0].Trim());

                Set(target, id);
            }
        }
    }

    private void Set(IPlayer? player, short id) {
        var classes = ContentService!.Classes;

        if (player is not null) {
            if (classes.Contains(id)) {
                player.Class = classes[id]!;
                player.Character.ClassCode = id;

                player!.AllocateAttributes();

                PacketSender!.SendAttributes(player);

                var instance = GetInstance(player);

                if (instance is not null) {
                    PacketSender!.SendPlayerDataTo(player, instance);
                    PacketSender!.SendPlayerVital(player, instance);
                }
                else {
                    PacketSender!.SendPlayerVital(player);
                }
            }
        }
        else {
            PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, Administrator!);
        }
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }
}