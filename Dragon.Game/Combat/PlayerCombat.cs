using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Attributes;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Instances;

using Dragon.Game.Combat.Handler;

namespace Dragon.Game.Combat;

public class PlayerCombat : IEntityCombat {
    public IPlayer Player { get; init; }
    public IPacketSender PacketSender { get; private set; }
    public IDatabase<Npc>? Npcs { get; private set; }
    public IDatabase<Skill>? Skills { get; private set; }
    public IDatabase<Effect>? Effects { get; private set; }
    public IDatabase<GroupAttribute>? NpcAttributes { get; private set; }
    public InstanceService? InstanceService { get; private set; }

    public CharacterSkill? Current { get; private set; }
    public Skill? CurrentData { get; private set; }

    public ISkill Healing { get; set; }
    public ISkill HoT { get; set; }
    public ISkill Damage { get; set; }
    public ISkill DoT { get; set; }
    public ISkill Effect { get; set; }
    public ISkill Aura { get; set; }

    public PlayerCombat(IPlayer player, IPacketSender sender, ContentService content, InstanceService instanceService) {
        Player = player;
        PacketSender = sender;
        InstanceService = instanceService;

        Npcs = content.Npcs;
        Skills = content.Skills;
        Effects = content.Effects;
        NpcAttributes = content.NpcAttributes;

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
            InstanceService = InstanceService
        };

        DoT = new DoT() {
            Player = player,
            Skills = Skills,
            PacketSender = sender,
            InstanceService = InstanceService
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

    public void Cast(int index) {
        if (Skills is not null) {
            var inventory = Player.Skills.Get(index);

            if (inventory is not null) {
                var id = inventory.SkillId;

                if (Skills.Contains(id)) {
                    var data = Skills[id]!;

                    if (!IsCostEnough(data.CostType, inventory.Cost)) {
                        PacketSender!.SendMessage(SystemMessage.InsuficientMana, QbColor.BrigthRed, Player!, new string[] { id.ToString() });

                        return;
                    }

                    if (!IsTargetInRange(inventory)) {
                        PacketSender!.SendMessage(SystemMessage.InvalidRange, QbColor.BrigthRed, Player!);

                        return;
                    }

                    if (CouldSelectTargetByPrimaryEffect(data)) {
                        Current = inventory;
                        CurrentData = data;

                        var target = new Target() {
                            Entity = Player!.Target,
                            Type = Player!.TargetType
                        };

                        ProcessSkill(inventory, data, target);
                    }
                }
            }
        }
    }

    private void ProcessSkill(CharacterSkill inventory, Skill data, Target target) {
        var instance = GetInstance();

        if (instance is not null) {
            foreach (var (type, effect) in inventory.Effects) {
                switch (type) {
                    case SkillEffectType.Damage:
                        if (!Damage.CouldSelect(target, effect)) {
                            PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!);

                            continue;
                        }

                        break;

                    case SkillEffectType.DoT:
                        if (!DoT.CouldSelect(target, effect)) {
                            PacketSender!.SendMessage(SystemMessage.InvalidTarget, QbColor.BrigthRed, Player!);

                            continue;
                        }

                        break;

                    case SkillEffectType.Steal:
                        break;
                }

                ProcessEffect(instance, type, effect, inventory, data, target);
            }
        }
    }

    private void ProcessEffect(IInstance instance, SkillEffectType type, SkillEffect? effect, CharacterSkill inventory, Skill data, Target target) {
        if (effect is not null) {
            IList<Target> targets;
            ISkill? handler = null;

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
        var r = Convert.ToInt32(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
        return r <= range;
    }

    private IInstance? GetInstance() {
        var instanceId = Player!.Character.Map;
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }
}