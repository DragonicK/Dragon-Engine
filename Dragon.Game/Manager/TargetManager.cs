using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Entity;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class TargetManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public ConnectionService? ConnectionService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly ChestManager ChestManager;

    public TargetManager(IServiceInjector injector) {
        injector.Inject(this);

        ChestManager = new ChestManager(injector);
    }

    public void ProcessTarget(IPlayer player, int index, TargetType targetType) {
        if (player.IsWarehouseOpen || player.ShopId > 0) {
            return;
        }

        var sender = GetPacketSender();
        var lastTargetType = player.TargetType;

        player.TargetType = targetType;
        player.Target = targetType == TargetType.None ? null : GetEntity(player, index, targetType);

        if (lastTargetType == TargetType.Chest) {
            if (targetType != TargetType.Chest) {
                CloseChest(player);
            }
        }

        if (player.TargetType == TargetType.Npc) {
            SelectEntityNpc(sender, player);
        }

        if (player.TargetType == TargetType.Chest) {
            OpenChest(player, index);
        }
    }

    private void SelectEntityNpc(IPacketSender sender, IPlayer player) {
        var entity = player.Target;

        if (entity is not null) {
            var npc = GetNpc(entity.Id);

            if (npc is not null) {
                if (npc.Behaviour != NpcBehaviour.Monster && npc.Behaviour != NpcBehaviour.Boss) {
                    if (npc.Conversations.Count > 0) {
                        sender.SendConversation(player, npc.Id);
                    }
                }
            }
        }
    }

    private void OpenChest(IPlayer player, int index) {
        ChestManager.OpenChest(player, index);
    }

    private void CloseChest(IPlayer player) {
        ChestManager.CloseChest(player);
    }

    private IEntity? GetEntity(IPlayer player, int index, TargetType targetType) {
        var instance = GetInstance(player);

        if (instance is not null) {
            if (targetType == TargetType.Player) {
                return instance.GetPlayer(index);
            }
            else if (targetType == TargetType.Npc) {
                return instance.Entities[--index];
            }
            else if (targetType == TargetType.Chest) {
                return instance.GetChest(index);
            }
        }

        return null;
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

    private Npc? GetNpc(int id) {
        var npcs = ContentService!.Npcs;

        npcs.TryGet(id, out var npc);

        return npc;
    }
}