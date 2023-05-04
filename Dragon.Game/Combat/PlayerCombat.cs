using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Repository;
using Dragon.Game.Combat.Death;
using Dragon.Game.Combat.Common;
using Dragon.Game.Combat.Handler;
using Dragon.Game.Network.Senders;
using Dragon.Game.Network;
using System.Security.Cryptography.X509Certificates;

namespace Dragon.Game.Combat;

public sealed class PlayerCombat : IEntityCombat {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public ConfigurationService? Configuration { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public ISkillHandler Healing { get; private set; }
    public ISkillHandler HoT { get; private set; }
    public ISkillHandler Damage { get; private set; }
    public ISkillHandler DoT { get; private set; }
    public ISkillHandler Effect { get; private set; }
    public ISkillHandler Aura { get; private set; }
    public IEntityDeath EntityDeath { get; private set; }
    public IEntityDeath PlayerDeath { get; private set; }

    public CharacterSkill? CurrentSlot { get; private set; }
    public Skill? CurrentData { get; private set; }

    public bool IsBufferedSkill { get; set; }
    public int BufferedSkillIndex { get; set; }
    public int BufferedSkillTime { get; set; }

    public readonly IPlayer Player;

    public PlayerCombat(IServiceInjector injector, IPlayer player) {
        injector.Inject(this);

        Player = player;

        EntityDeath = new EntityDeath(injector);
        PlayerDeath = new PlayerDeath(injector);

        HoT = new HoT(injector, player);
        Aura = new Aura(injector, player);
        Effect = new Buff(injector, player);
        Healing = new Healing(injector, player);
        DoT = new DoT(injector, PlayerDeath, EntityDeath, player);
        Damage = new Damage(injector, PlayerDeath, EntityDeath, player);
    }

    public void BufferSkill(int slotIndex) {   
        var inventory = Player.Skills.Get(slotIndex);

        if (inventory is not null) {
            var sender = GetPacketSender();
            var id = inventory.SkillId;

            GetDatabaseSkills().TryGet(id, out var data);

            if (data is not null) {
                if (IsTargetDead()) {
                    sender.SendClearCast(Player);
                    sender.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                    return;
                }

                if (!CouldCastOnSelf(inventory)) {
                    sender.SendClearCast(Player);
                    sender.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                    return;
                }

                if (!IsCostEnough(data.CostType, inventory.Cost)) {
                    sender.SendClearCast(Player);
                    sender.SendMessage(SystemMessage.InsuficientMana, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                    return;
                }

                if (!IsTargetInRange(inventory)) {
                    sender.SendClearCast(Player);
                    sender.SendMessage(SystemMessage.InvalidRange, QbColor.BrigthRed, Player!);

                    return;
                }

                sender.SendAnimation(GetInstance()!, data.CastAnimationId, Player.GetX(), Player.GetY(), TargetType.Player, Player.IndexOnInstance, true);

                IsBufferedSkill = true;
                BufferedSkillIndex = slotIndex;
                BufferedSkillTime = Environment.TickCount + (inventory.CastTime * 1000);
            }
        }
    }

    public void CastSkill(int slotIndex) {
        var inventory = Player.Skills.Get(slotIndex);

        if (inventory is not null) {
            var sender = GetPacketSender();
            var id = inventory.SkillId;

            GetDatabaseSkills().TryGet(id, out var data);

            if (data is not null) {

                if (IsTargetDead()) {
                    sender.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                    return;
                }

                if (!CouldCastOnSelf(inventory)) {
                    sender.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                    return;
                }

                if (!IsCostEnough(data.CostType, inventory.Cost)) {
                    sender.SendMessage(SystemMessage.InsuficientMana, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                    return;
                }

                if (!IsTargetInRange(inventory)) {
                    sender.SendMessage(SystemMessage.InvalidRange, QbColor.BrigthRed, Player!);

                    return;
                }

                if (CouldSelectTargetByPrimaryEffect(data)) {
                    CurrentData = data;
                    CurrentSlot = inventory;

                    var target = new Target() {
                        Entity = Player.Target,
                        Type = Player.TargetType
                    };

                    ExecuteSkill(inventory, data, ref target, slotIndex);
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

    private void ExecuteSkill(CharacterSkill inventory, Skill data, ref Target target, int inventoryIndex) {
        var instance = GetInstance();

        if (instance is not null) {
            var sender = GetPacketSender();

            foreach (var (type, effect) in inventory.Effects) {
                switch (type) {
                    case SkillEffectType.Damage:
                        if (!Damage.CanSelect(ref target, effect)) {
                            sender.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!);

                            continue;
                        }

                        break;

                    case SkillEffectType.DoT:
                        if (!DoT.CanSelect(ref target, effect)) {
                            sender.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!);

                            continue;
                        }

                        break;

                    case SkillEffectType.Steal:
                        break;
                }

                ExecuteEffect(instance, type, effect, inventory, data, ref target, inventoryIndex);
            }
        }
    }

    private void ExecuteEffect(IInstance instance, SkillEffectType type, SkillEffect? effect, CharacterSkill inventory, Skill data, ref Target target, int inventoryIndex) {
        if (effect is not null) {
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
               var targets = handler.GetTarget(ref target, instance, inventory, effect);

                if (targets.Count > 0) {
                    var sender = GetPacketSender();

                    for (var i = 0; i < targets.Count; ++i) {
                        var enemy = targets[i];
                        var entity = enemy.Entity;

                        if (entity is not null) {
                            var x = entity.GetX();
                            var y = entity.GetY();
                            var lockType = enemy.Type;
                            var index = entity.IndexOnInstance;

                            sender.SendSkillCooldown(Player, inventoryIndex);
                            sender.SendAnimation(instance, data.AttackAnimationId, x, y, lockType, index, false);

                            var damaged = handler.GetDamage(ref enemy, inventory, type);

                            handler.Inflict(ref damaged, ref enemy, instance, effect);
                        }
                    }

                    handler.ResetTargets();
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
        if (Player.TargetType != TargetType.None) {
            if (Player.TargetType != TargetType.Chest) {
                var target = Player.Target;

                if (target is not null) {
                    return target.Vitals.Get(Vital.HP) <= 0;
                }
            }
        }

        return true;
    }

    private bool IsTargetInRange(int range) {
        if (Player.TargetType != TargetType.None) {
            var target = Player.Target;

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

        return Player.Vitals.Get((Vital)index) >= cost;
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
        var instanceId = Player.Character.Map;
        var instances = InstanceService!.Instances;

        instances.TryGetValue(instanceId, out var instance);

        return instance;
    }

    private bool CouldCastOnSelf(CharacterSkill inventory) {
        if (Player.TargetType == TargetType.Player) {
            var target = Player.Target;

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

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }

    private IDatabase<Skill> GetDatabaseSkills() {
        return ContentService!.Skills;
    }
}