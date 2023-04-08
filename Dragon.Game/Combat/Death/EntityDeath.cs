using Dragon.Core.Model;
using Dragon.Core.Model.Entity;

using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Configurations;

namespace Dragon.Game.Combat.Death;

public class EntityDeath : IEntityDeath {

    public IPacketSender? PacketSender { get; init; }
    public ContentService? ContentService { get; init; }
    public IConfiguration? Configuration { get; init; }
    public InstanceService? InstanceService { get; init; }

    private ExperienceHandler? expHandler;

    public void Execute(IEntity attacker, IEntity receiver) {
        if (expHandler is null) {
            expHandler = new ExperienceHandler() {
                Configuration = Configuration,
                ContentService = ContentService
            };
        }

        var player = attacker as IPlayer;
        var entity = receiver as IInstanceEntity;

        if (player is not null && entity is not null) {
            var instance = GetInstance(player)!;

            entity.IsDead = true;

            entity.Vitals.Set(Vital.HP, 0);
            entity.Vitals.Set(Vital.MP, 0);
            entity.Vitals.Set(Vital.Special, 0);

            ClearEntityTarget(entity);
            GiveAttackerExperience(player, entity, instance);
            ClearInstancePlayerTargets(player, entity, instance);

            // remove effects
            // send box loot
        }
    }

    private void ClearEntityTarget(IInstanceEntity entity) {
        entity.Target = null;
        entity.TargetType = TargetType.None;
    }

    private void ClearInstancePlayerTargets(IPlayer attacker, IInstanceEntity entity, IInstance instance) {
        foreach (var player in instance.GetPlayers()) {
            if (player != attacker) {
                if (player.TargetType == TargetType.Npc) {
                    if (player.Target == entity) {
                        player.Target = null;
                        player.TargetType = TargetType.None;

                        PacketSender!.SendTarget(player, TargetType.None, 0);
                    }
                }
            }
        }
    }

    private void GiveAttackerExperience(IPlayer player, IInstanceEntity entity, IInstance instance) {
        var exp = expHandler!.GetExperience(player, entity);

        if (exp > 0) {
            player.Character.Experience += exp;

            if (expHandler.CheckForLevelUp(player)) { 
                player.AllocateAttributes();

                PacketSender!.SendAttributes(player);
                PacketSender!.SendPlayerVital(player, instance);
                PacketSender!.SendPlayerDataTo(player, instance);
            }

            SendExperience(player);
        }
    }

    private void SendExperience(IPlayer player) {
        var level = player.Character.Level;
        var experience = ContentService!.PlayerExperience;

        var maximum = 0;

        if (level > 0) {
            if (level <= experience.MaximumLevel) {
                maximum = experience.Get(level);
            }
        }

        var minimum = player.Character.Experience;

        if (minimum >= maximum) {
            minimum = maximum;

            player.Character.Experience = minimum;
        }

        PacketSender!.SendExperience(player, minimum, maximum);
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player!.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }
}