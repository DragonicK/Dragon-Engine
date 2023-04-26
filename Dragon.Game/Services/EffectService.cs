using Dragon.Core.Services;
using Dragon.Core.Model.DisplayIcon;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Services;

public sealed class EffectService : IService, IUpdatableService {
    public ServicePriority Priority => ServicePriority.Last;
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int AuraTick = 3000;

    private int deltaCount;

    public void Start() {

    }

    public void Stop() {

    }

    public void Update(int deltaTime) {
        deltaCount += deltaTime;

        var task = Task.Run(DecreaseDuration);

        if (deltaCount >= AuraTick) {
            deltaCount = 0;

            task.Wait();

            Task.Run(ProcessAura);
        }
    }

    private void DecreaseDuration() {
        var repository = ConnectionService!.PlayerRepository;
        var sender = PacketSenderService!.PacketSender;

        if (repository is not null) {
            var players = repository.GetPlayers();

            foreach (var (_, player) in players) {
                if (player is not null) {
                    if (player.InGame) {
                        DecreaseDuration(player, sender!);
                    }
                }
            }
        }
    }

    private void DecreaseDuration(IPlayer player, IPacketSender sender) {
        var list = player.Effects.ToList();

        for (var i = 0; i < list.Count; i++) {
            var element = list[i];
            var id = element.EffectId;
            var isAura = element.IsAura;

            if (id > 0 && !isAura) {
                element.EffectDuration--;

                if (element.EffectDuration < 0) {
                    element.EffectId = 0;
                    element.EffectLevel = 0;
                    element.EffectDuration = 0;

                    player.Effects.UpdateAttributes();
                    player.AllocateAttributes();

                    Remove(player, sender, id);
                }
            }
        }
    }

    private void Remove(IPlayer player, IPacketSender sender, int id) {
        var instance = GetInstance(player);

        if (instance is not null) {
            var icon = new DisplayIcon() {
                Id = id,
                Type = DisplayIconType.Effect,
                OperationType = DisplayIconOperation.Remove
            };

            sender.SendAttributes(player);
            sender.SendPlayerVital(player, instance);

            sender.SendDisplayIcon(ref icon, DisplayIconTarget.Player, player, instance);
        }
    }

    private void ProcessAura() {
        var repository = ConnectionService!.PlayerRepository;
        var sender = PacketSenderService!.PacketSender;

        if (repository is not null) {
            var players = repository.GetPlayers();

            foreach (var (_, player) in players) {
                if (player is not null) {
                    if (player.InGame) {
                        if (player.Auras.Count > 0) {
                            ProcessAura(player, sender!);
                        }
                    }
                }
            }
        }
    }

    private void ProcessAura(IPlayer player, IPacketSender sender) {
        var manager = new AuraManager() {
            Player = player,
            PacketSender = sender,
            Effects = ContentService!.Effects,
            InstanceService = InstanceService
        };

        manager.CheckPartyMemberAuras();
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