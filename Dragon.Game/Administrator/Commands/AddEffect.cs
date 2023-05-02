using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Core.Model.DisplayIcon;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Administrator.Commands;

public sealed class AddEffect : IAdministratorCommand {
    public AdministratorCommands Command => AdministratorCommands.AddEffect;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int MaximumParameters = 4;

    public AddEffect(IServiceInjector injector) {
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
            _ = int.TryParse(parameters[2], out var level);
            _ = int.TryParse(parameters[3], out var duration);

            var repository = ConnectionService!.PlayerRepository;
            var target = repository!.FindByName(parameters[0].Trim());

            Add(administrator, target, id, level, duration);
        }
    }

    private void Add(IPlayer administrator, IPlayer? player, int id, int level, int duration) {
        var sender = GetPacketSender();
        var effects = ContentService!.Effects;

        if (player is not null) {
            var instance = GetInstance(player);

            if (effects.Contains(id)) {
                player.Effects.Add(id, level, duration);
                player.Effects.UpdateAttributes();

                player.AllocateAttributes();

                sender.SendAttributes(player);

                if (instance is not null) {
                    sender.SendPlayerVital(player, instance);
                }
                else {
                    sender.SendPlayerVital(player);
                }

                SendDisplayIcon(instance, player, id, level, duration);
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.Red, administrator);
        }
    }

    private void SendDisplayIcon(IInstance? instance, IPlayer player, int id, int level, int duration) {
        if (instance is not null) {
            var icon = new DisplayIcon() {
                Id = id,
                Level = level,
                Duration = duration,
                Type = DisplayIconType.Effect,
                DurationType = DisplayIconDuration.Limited,
                ExhibitionType = DisplayIconExhibition.Player,
                OperationType = DisplayIconOperation.Update,
                SkillType = DisplayIconSkill.None
            };

            GetPacketSender().SendDisplayIcon(ref icon, DisplayIconTarget.Player, player, instance);
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