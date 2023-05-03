using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Core.Model.Entity;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Combat.Death;

public sealed class EntityDeath : IEntityDeath {
    public LoggerService? LoggerService { get; private set; }
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly ChestManager ChestManager;
    private readonly ExperienceHandler ExpHandler;

    public EntityDeath(IServiceInjector injector) {
        injector.Inject(this);

        ChestManager = new ChestManager(injector);
        ExpHandler = new ExperienceHandler(injector);
    }
    
    public void Execute(IEntity attacker, IEntity receiver) {
        var player = attacker as IPlayer;
        var entity = receiver as IInstanceEntity;

        if (player is not null && entity is not null) {
            var logger = GetLogger();
            var sender = GetPacketSender();
            var instance = GetInstance(player);

            if (instance is not null) {
                entity.IsDead = true;

                entity.Vitals.Set(Vital.HP, 0);
                entity.Vitals.Set(Vital.MP, 0);
                entity.Vitals.Set(Vital.Special, 0);

                ClearEntityTarget(entity);
                GiveAttackerExperience(sender, player, entity, instance);
                ClearInstancePlayerTargets(sender, player, entity, instance);

                // remove effects

                var chest = ChestManager.CreateInstanceChest(player, entity, instance);

                if (chest is not null) {
                    if (chest.Items.Count > 0) {
                        var index = instance.Add(chest);

                        if (index > 0) {
                            sender.SendChest(instance, chest);
                        }
                        else {
                            logger.Write(WarningLevel.Warning, GetType().Name, "Failed to add chest to instance.");
                        }
                    }
                }
            }
        }
    }

    private void ClearEntityTarget(IInstanceEntity entity) {
        entity.Target = null;
        entity.TargetType = TargetType.None;
    }

    private void ClearInstancePlayerTargets(IPacketSender sender, IPlayer attacker, IInstanceEntity entity, IInstance instance) {
        foreach (var player in instance.GetPlayers()) {
            if (player != attacker) {
                if (player.TargetType == TargetType.Npc) {
                    if (player.Target == entity) {
                        player.Target = null;
                        player.TargetType = TargetType.None;

                        sender.SendTarget(player, TargetType.None, 0);
                    }
                }
            }
        }
    }

    private void GiveAttackerExperience(IPacketSender sender, IPlayer player, IInstanceEntity entity, IInstance instance) {
        var exp = ExpHandler.GetExperience(player, entity);

        if (exp > 0) {
            player.Character.Experience += exp;

            if (ExpHandler.CheckForLevelUp(player)) { 
                player.AllocateAttributes();

                sender.SendAttributes(player);
                sender.SendPlayerVital(player, instance);
                sender.SendPlayerDataTo(player, instance);
            }

            SendExperience(sender, player);
        }
    }

    private void SendExperience(IPacketSender sender, IPlayer player) {
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

        sender.SendExperience(player, minimum, maximum);
    }

    private IInstance? GetInstance(IPlayer player) {
        var instanceId = player!.Character.Map;
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);
      
        return instance;
    }

    private ILogger GetLogger() {
        return LoggerService!.Logger!;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}