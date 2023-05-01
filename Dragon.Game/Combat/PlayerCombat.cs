using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;

using Dragon.Game.Combat.Common;
using Dragon.Game.Combat.Handler;
using Dragon.Game.Configurations;
using Dragon.Game.Combat.Death;
using Dragon.Game.Repository;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Combat;

public class PlayerCombat : IEntityCombat {
    public IPlayer Player { get; init; }
    public IConfiguration Configuration { get; init; }
    public IPlayerRepository? PlayerRepository { get; init; }
    public IPacketSender PacketSender { get; private set; }
    public IDatabase<Npc>? Npcs { get; private set; }
    public IDatabase<Skill>? Skills { get; private set; }
    public IDatabase<Effect>? Effects { get; private set; }
    public IDatabase<GroupAttribute>? NpcAttributes { get; private set; }
    public InstanceService? InstanceService { get; private set; }

    public CharacterSkill? CurrentSlot { get; private set; }
    public Skill? CurrentData { get; private set; }

    public ISkillHandler Healing { get; private set; }
    public ISkillHandler HoT { get; private set; }
    public ISkillHandler Damage { get; private set; }
    public ISkillHandler DoT { get; private set; }
    public ISkillHandler Effect { get; private set; }
    public ISkillHandler Aura { get; private set; }
    public IEntityDeath EntityDeath { get; private set; }
    public IEntityDeath PlayerDeath { get; private set; }

    public bool IsBufferedSkill { get; set; }
    public int BufferedSkillIndex { get; set; }
    public int BufferedSkillTime { get; set; }

    public PlayerCombat(IPlayer player, IConfiguration configuration, IPacketSender sender, ContentService contentService, InstanceService instanceService) {
        Player = player;
        PacketSender = sender;
        Configuration = configuration;
        InstanceService = instanceService;

        Npcs = contentService.Npcs;
        Skills = contentService.Skills;
        Effects = contentService.Effects;
        NpcAttributes = contentService.NpcAttributes;

        EntityDeath = new EntityDeath() {
            PacketSender = sender,
            Configuration = configuration,
            ContentService = contentService,
            InstanceService = instanceService,
            PlayerRepository = PlayerRepository
        };

        PlayerDeath = new PlayerDeath() {
            PacketSender = sender,
            Configuration = configuration,
            ContentService = contentService,
            InstanceService = instanceService,
            PlayerRepository = PlayerRepository
        };

        Healing = new Healing() {
            Player = player,
            Skills = Skills,
            PacketSender = sender,
            InstanceService = InstanceService
        };

        HoT = new HoT() {
            Player = player,
            Skills = Skills,
            PacketSender = sender,
            InstanceService = InstanceService
        };

        Damage = new Damage() {
            Player = player,
            Skills = Skills,
            PacketSender = sender,
            InstanceService = InstanceService,
            EntityDeath = EntityDeath, 
            PlayerDeath = PlayerDeath
        };

        DoT = new DoT() {
            Player = player,
            Skills = Skills,
            PacketSender = sender,
            InstanceService = InstanceService,
            EntityDeath = EntityDeath,
            PlayerDeath = PlayerDeath
        };

        Effect = new Buff() {
            Player = player,
            Skills = Skills,
            Effects = Effects,
            PacketSender = sender,
            InstanceService = InstanceService
        };

        Aura = new Aura() {
            Player = player,
            Effects = Effects,
            PacketSender = sender,
            InstanceService = InstanceService
        };
    }

    public void BufferSkill(int slotIndex) {
        if (Skills is not null) {
            var inventory = Player.Skills.Get(slotIndex);

            if (inventory is not null) {
                var id = inventory.SkillId;

                if (Skills.Contains(id)) {
                    var data = Skills[id]!;

                    if (IsTargetDead()) {
                        PacketSender!.SendClearCast(Player);
                        PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                        return;
                    }

                    if (!CouldCastOnSelf(inventory)) {
                        PacketSender!.SendClearCast(Player);
                        PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                        return;
                    }

                    if (!IsCostEnough(data.CostType, inventory.Cost)) {
                        PacketSender!.SendClearCast(Player);
                        PacketSender!.SendMessage(SystemMessage.InsuficientMana, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                        return;
                    }

                    if (!IsTargetInRange(inventory)) {
                        PacketSender!.SendClearCast(Player);
                        PacketSender!.SendMessage(SystemMessage.InvalidRange, QbColor.BrigthRed, Player!);

                        return;
                    }

                    PacketSender.SendAnimation(GetInstance()!, data.CastAnimationId, Player.GetX(), Player.GetY(), TargetType.Player, Player.IndexOnInstance, true);

                    IsBufferedSkill = true;
                    BufferedSkillIndex = slotIndex;
                    BufferedSkillTime = Environment.TickCount + (inventory.CastTime * 1000);
                }
            }
        }
    }

    public void CastSkill(int slotIndex) {
        if (Skills is not null) {
            var inventory = Player.Skills.Get(slotIndex);

            if (inventory is not null) {
                var id = inventory.SkillId;

                if (Skills.Contains(id)) {
                    var data = Skills[id]!;

                    if (IsTargetDead()) {
                        PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                        return;
                    }

                    if (!CouldCastOnSelf(inventory)) {
                        PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                        return;
                    }

                    if (!IsCostEnough(data.CostType, inventory.Cost)) {
                        PacketSender!.SendMessage(SystemMessage.InsuficientMana, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                        return;
                    }

                    if (!IsTargetInRange(inventory)) {
                        PacketSender!.SendMessage(SystemMessage.InvalidRange, QbColor.BrigthRed, Player!);

                        return;
                    }         

                    if (CouldSelectTargetByPrimaryEffect(data)) {
                        CurrentSlot = inventory;
                        CurrentData = data;

                        var target = new Target() {
                            Entity = Player!.Target,
                            Type = Player!.TargetType
                        };

                        ExecuteSkill(inventory, data, target, slotIndex);
                    }
                }
            }
        }
    }

    public void ClearBufferedSkill() {
        IsBufferedSkill = false;
        BufferedSkillIndex = 0;
        BufferedSkillTime = 0;

        CurrentSlot = null;
        CurrentData = null;
    }

    private void ExecuteSkill(CharacterSkill inventory, Skill data, Target target, int inventoryIndex) {
        var instance = GetInstance();

        if (instance is not null) {
            foreach (var (type, effect) in inventory.Effects) {
                switch (type) {
                    case SkillEffectType.Damage:
                        if (!Damage.CanSelect(target, effect)) {
                            PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!);

                            continue;
                        }

                        break;

                    case SkillEffectType.DoT:
                        if (!DoT.CanSelect(target, effect)) {
                            PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!);

                            continue;
                        }

                        break;

                    case SkillEffectType.Steal:
                        break;
                }

                ExecuteEffect(instance, type, effect, inventory, data, target, inventoryIndex);
            }
        }
    }

    private void ExecuteEffect(IInstance instance, SkillEffectType type, SkillEffect? effect, CharacterSkill inventory, Skill data, Target target, int inventoryIndex) {
        if (effect is not null) {
            IList<Target> targets;
            ISkillHandler? handler = null;

            switch (type) {
                case SkillEffectType.Aura: handler = Aura; break;
                case SkillEffectType.Buff: handler = Effect; break;
                case SkillEffectType.Damage: handler = Damage; break;
                case SkillEffectType.Heal: handler = Healing; break;
                case SkillEffectType.HoT: handler = HoT; break;
                case SkillEffectType.DoT: handler = DoT; break;
                case SkillEffectType.Steal: break;
                case SkillEffectType.Silence: break;
            }

            if (handler is not null) {
                targets = handler.GetTarget(target, instance, inventory, effect);

                if (targets.Count > 0) {
                    for (var i = 0; i < targets.Count; ++i) {
                        var lockType = targets[i].Type;
                        var x = targets[i].Entity.GetX();
                        var y = targets[i].Entity.GetY();
                        var index = targets[i].Entity.IndexOnInstance;

                        PacketSender.SendSkillCooldown(Player, inventoryIndex);
                        PacketSender.SendAnimation(instance, data.AttackAnimationId, x, y, lockType, index, false);

                        var damaged = handler.GetDamage(targets[i], inventory, type);

                        handler.Inflict(damaged, targets[i], instance, effect);
                    }
                }
            }
        }
    }

    private bool CouldSelectTargetByPrimaryEffect(Skill data) {
        var type = data.EffectType;

        switch (type) {
            case SkillEffectType.Damage:
                break;
            case SkillEffectType.DoT:
                break;
            case SkillEffectType.Steal:
                break;
            case SkillEffectType.Silence:
                break;
            case SkillEffectType.Blind:
                break;
            case SkillEffectType.Dispel:
                break;
            case SkillEffectType.Immobilize:
                break;
        }

        return true;
    }

    private bool IsTargetInRange(CharacterSkill inventory) {
        switch (inventory.TargetType) {
            case SkillTargetType.Caster:
                return true;

            case SkillTargetType.Single:
                return IsTargetInRange(inventory.Range);

            case SkillTargetType.AoE:
                return true;

            case SkillTargetType.Group:
                return true;
        }

        return false;
    }

    private bool IsTargetDead() {
        if (Player!.TargetType != TargetType.None) {
            if (Player!.TargetType != TargetType.Chest) {
                var target = Player!.Target;

                if (target is not null) {
                    return target.Vitals.Get(Vital.HP) <= 0;
                }
            }
        }

        return true;
    }

    private bool IsTargetInRange(int range) {
        if (Player!.TargetType != TargetType.None) {
            var target = Player!.Target;

            if (target is not null) {
                var x = Player.Character.X;
                var y = Player.Character.Y;

                var _targetX = target.GetX();
                var _targetY = target.GetY();

                return IsInRange(range, x, y, _targetX, _targetY);
            }
        }

        return false;
    }

    private bool IsCostEnough(SkillCostType costType, int cost) {
        if (costType == SkillCostType.None) {
            return true;
        }

        var index = GetFromCostType(costType);

        if (index is null) {
            return true;
        }

        return Player!.Vitals.Get((Vital)index) >= cost;
    }

    private Vital? GetFromCostType(SkillCostType costType) => costType switch {
        SkillCostType.None => null,
        SkillCostType.HP => Vital.HP,
        SkillCostType.MP => Vital.MP,
        SkillCostType.Special => Vital.Special,
        _ => null
    };

    private bool IsInRange(int range, int x1, int y1, int x2, int y2) {
        return Convert.ToInt32(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2))) <= range;
    }

    private IInstance? GetInstance() {
        var instanceId = Player!.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }

    private bool CouldCastOnSelf(CharacterSkill inventory) {
        if (Player!.TargetType == TargetType.Player) {
            var target = Player!.Target;

            if (target == Player) {
                var effects = inventory.Effects;

                if (effects.ContainsKey(SkillEffectType.Damage)) {
                    return false;
                }

                if (effects.ContainsKey(SkillEffectType.DoT)) {
                    return false;
                }

                if (effects.ContainsKey(SkillEffectType.Immobilize)) {
                    return false;
                }

                if (effects.ContainsKey(SkillEffectType.Dispel)) {
                    return false;
                }

                if (effects.ContainsKey(SkillEffectType.Silence)) {
                    return false;
                }
            }
        }

        return true;
    }
}