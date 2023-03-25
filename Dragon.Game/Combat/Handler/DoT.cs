using Dragon.Core.Content;
using Dragon.Core.Model;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Characters;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Instances;

namespace Dragon.Game.Combat.Handler;

public class DoT : ICombatHandler {
    public IPlayer? Player { get; set; }
    public IDatabase<Skill>? Skills { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }

    public bool CanSelect(Target target, SkillEffect effect) {
        if (Player!.Target is IInstanceEntity entity) {
            if (entity is not null) {
                return entity.Behaviour == NpcBehaviour.Monster || entity.Behaviour == NpcBehaviour.Boss;
            }
        }

        return false;
    }

    public Damaged GetDamage(Target target, CharacterSkill inventory, SkillEffectType type) {
        var level = inventory.SkillLevel;
        var source = inventory.Effects[type];

        var dAmplification = inventory.Amplification;

        var pAmplification = Player!.Attributes.Get(UniqueAttribute.Amplification);
        var pPotency = Player!.Attributes.Get(UniqueAttribute.FinalDamage);

        var damage = source.Damage + (source.DamagePerLevel * level);

        var final = damage * (pAmplification + dAmplification);

        if (inventory.AttributeType == SkillAttributeType.Physic) {
            final += Player!.Attributes.Get(SecondaryAttribute.Attack) - target.Entity.Attributes.Get(SecondaryAttribute.Defense);
        }
        else if (inventory.AttributeType == SkillAttributeType.Magic) {
            final += Player!.Attributes.Get(SecondaryAttribute.MagicAttack) - target.Entity.Attributes.Get(SecondaryAttribute.MagicDefense);
        }

        return new Damaged() {
            Value = Convert.ToInt32(final * (pPotency + 1f)),
            Type = DamagedType.Damage
        };
    }

    public IList<Target> GetTarget(Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect) {
        var list = new List<Target>();

        var targetType = effect.TargetType;
        var range = inventory.Range;

        switch (targetType) {
            case SkillTargetType.Single:
                if (CanSelect(target, effect)) {
                    list.Add(target);
                }

                break;
            case SkillTargetType.AoE:
                SetAoETarget(list, instance, target, range);

                break;
        }

        return list;
    }

    public void Inflict(Damaged damaged, Target target, IInstance instance, SkillEffect effect) {
        if (target.Entity is not null) {
            var vital = GetFromVitalType(effect.VitalType);

            var value = target.Entity.Vitals.Get(vital);

            target.Entity.Vitals.Set(vital, value - damaged.Value);

            PacketSender!.SendInstanceEntityVital(instance, target.Entity.IndexOnInstance);

            SendDamage(vital, damaged.Value, target.Entity, instance);
        }
    }

    private void SetAoETarget(IList<Target> list, IInstance instance, Target primary, int range) {
        if (primary.Entity is not null) {
            var x1 = primary.Entity.GetX();
            var y1 = primary.Entity.GetY();

            int x2, y2;

            var entities = instance.Entities;

            foreach (var entity in entities) {
                if (!entity.IsDead) {
                    x2 = entity.X;
                    y2 = entity.Y;

                    if (entity.Behaviour == NpcBehaviour.Monster || entity.Behaviour == NpcBehaviour.Boss) {
                        if (IsInRange(range, x1, y1, x2, y2)) {
                            list.Add(new Target() {
                                Entity = (IEntity)entity,
                                Type = TargetType.Npc
                            });
                        }
                    }
                }
            }
        }
    }

    private bool IsInRange(int range, int x1, int y1, int x2, int y2) {
        var r = Convert.ToInt32(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
        return r <= range;
    }

    private Vital GetFromVitalType(SkillVitalType vitalType) => vitalType switch {
        SkillVitalType.Hp => Vital.HP,
        SkillVitalType.Mp => Vital.MP,
        SkillVitalType.Special => Vital.Special,
        _ => Vital.HP
    };

    private void SendDamage(Vital vital, int value, IEntity entity, IInstance instance) {
        var damage = new Messages.Damage() {
            X = entity.GetX(),
            Y = entity.GetY(),
            Color = GetColor(vital),
            Message = value.ToString(),
            FontType = ActionMessageFontType.Damage,
            MessageType = ActionMessageType.Scroll
        };

        PacketSender!.SendMessage(damage, instance);
    }

    private QbColor GetColor(Vital vital) => vital switch {
        Vital.HP => QbColor.BrigthRed,
        Vital.MP => QbColor.DeepSkyBlue,
        Vital.Special => QbColor.Coral,
        _ => QbColor.HealingGreen
    };
}