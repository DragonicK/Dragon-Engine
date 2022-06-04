using Dragon.Core.Model;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Npcs;

using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Instances;
using Dragon.Game.Network;

namespace Dragon.Game.Manager;

public class TargetManager {
    public IPlayer? Player { get; init; }
    public InstanceService? InstanceService { get; init; }
    public ContentService? ContentService { get; init; }
    public IPacketSender? PacketSender { get; init; }

    public void ProcessTarget(int index, TargetType targetType) {
        Player!.TargetType = targetType;
        Player!.Target = targetType == TargetType.None ? null : GetEntity(index, targetType);

        if (Player!.IsWarehouseOpen) {
            return;
        }

        if (Player!.ShopId > 0) {
            return;
        }

        if (Player!.TargetType == TargetType.Npc) {
            var entity = Player!.Target;

            if (entity is not null) {
                var npc = GetNpc(entity.Id);

                if (npc is not null) {
                    if (npc.Behaviour != NpcBehaviour.Monster && npc.Behaviour != NpcBehaviour.Boss) {
                        if (npc.Conversations.Count > 0) {
                            PacketSender!.SendConversation(Player!, npc.Id);
                        }
                    }
                }
            }
        }
    }

    private IEntity? GetEntity(int index, TargetType targetType) {
        var instance = GetInstance();

        if (instance is not null) {
            if (targetType == TargetType.Player) {
                return instance.Get(index) as IEntity;
            }
            else if (targetType == TargetType.Npc) {
                index--;

                return instance.Entities[index] as IEntity;
            }
        }

        return null;
    }

    private IInstance? GetInstance() {
        var instanceId = Player!.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }

    private Npc? GetNpc(int id) {
        var npcs = ContentService!.Npcs;

        if (npcs is not null) {
            return npcs[id];
        }

        return null;
    }
}