using Dragon.Core.Content;
using Dragon.Core.Model;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Characters;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Combat.Common;

namespace Dragon.Game.Combat.Handler;

public class Healing : ISkillHandler {
    public IPlayer? Player { get; set; }
    public IDatabase<Skill>? Skills { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }

    public bool CanSelect(Target target, SkillEffect effect) {
        if (Player!.Target is IInstanceEntity entity) {
            if (entity is not null) {
                return entity.Behaviour != NpcBehaviour.Monster && entity.Behaviour != NpcBehaviour.Boss;
            }
        }

        if (Player!.Target is IPlayer) {
            return true;
        }

        return false;
    }

    public Damaged GetDamage(Target target, CharacterSkill inventory, SkillEffectType type) {
        var level = inventory.SkillLevel;
        var source = inventory.Effects[type];

        var dAmplification = inventory.Amplification;

        var pAmplification = Player!.Attributes.Get(UniqueAttribute.Amplification);
        var pPotency = Player!.Attributes.Get(UniqueAttribute.HealingPower);

        var damage = source.Damage + (source.DamagePerLevel * level);

        var healing = damage * (pAmplification + pPotency + dAmplification);

        if (inventory.AttributeType == SkillAttributeType.Physic) {
            healing += Player!.Attributes.Get(SecondaryAttribute.Attack);
        }
        else if (inventory.AttributeType == SkillAttributeType.Magic) {
            healing += Player!.Attributes.Get(SecondaryAttribute.MagicAttack);
        }

        return new Damaged() {
            Value = Convert.ToInt32(healing),
            Type = DamagedType.Heal
        };
    }

    public IList<Target> GetTarget(Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect) {
        var list = new List<Target>();

        var targetType = effect.TargetType;
        var range = inventory.Range;

        switch (targetType) {
            case SkillTargetType.Caster:
                list.Add(new Target() {
                    Entity = Player!,
                    Type = TargetType.Player
                });

                break;
            case SkillTargetType.Single:
                if (CanSelect(target, effect)) {
                    list.Add(target);
                }
                else {
                    list.Add(new Target() {
                        Entity = Player!,
                        Type = TargetType.Player
                    });
                }

                break;
            case SkillTargetType.AoE:
                if (CanSelect(target, effect)) {
                    list.Add(target);
                }
                else {
                    list.Add(new Target() {
                        Entity = Player!,
                        Type = TargetType.Player
                    });
                }

                SetAoETarget(list, instance, target, range);

                break;
            case SkillTargetType.Group:
                list.Add(new Target() {
                    Entity = Player!,
                    Type = TargetType.Player
                });

                SetGroupTarget(list, range);

                break;
        }

        return list;
    }

    public void Inflict(Damaged damaged, Target target, IInstance instance, SkillEffect effect) {
        if (target.Entity is not null) {
            var vital = GetFromVitalType(effect.VitalType);

            target.Entity.Vitals.Add(vital, damaged.Value);

            SendHealing(vital, damaged.Value, target.Entity, instance);
        }
    }

    private void SetAoETarget(IList<Target> list, IInstance instance, Target primary, int range) {
        if (primary.Entity is not null) {
            var players = instance.GetPlayers();

            var x1 = primary.Entity.GetX();
            var y1 = primary.Entity.GetY();

            int x2, y2;

            foreach (var player in players) {
                if (player is not null) {
                    if (player != Player) {
                        x2 = player.Character.X;
                        y2 = player.Character.Y;

                        if (IsInRange(range, x1, y1, x2, y2)) {
                            list.Add(new Target() {
                                Entity = player,
                                Type = TargetType.Player
                            });
                        }
                    }
                }
            }

            var entities = instance.Entities;

            foreach (var entity in entities) {
                if (!entity.IsDead) {
                    x2 = entity.X;
                    y2 = entity.Y;

                    if (entity.Behaviour != NpcBehaviour.Monster && entity.Behaviour != NpcBehaviour.Boss) {
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

    private void SetGroupTarget(IList<Target> list, int range) {
        var party = GetPartyManager();

        if (party is not null) {
            var x1 = Player!.Character.X;
            var y1 = Player!.Character.Y;

            var members = party.Members;

            foreach (var member in members) {
                if (!member.Disconnected) {
                    if (member.Player is not null) {
                        if (member.Player != Player) {

                            var x2 = member.Player.Character.X;
                            var y2 = member.Player.Character.Y;

                            if (IsInRange(range, x1, y1, x2, y2)) {
                                list.Add(new Target() {
                                    Entity = member.Player,
                                    Type = TargetType.Player
                                });
                            }

                        }
                    }
                }
            }
        }
    }

    private Vital GetFromVitalType(SkillVitalType vitalType) => vitalType switch {
        SkillVitalType.Hp => Vital.HP,
        SkillVitalType.Mp => Vital.MP,
        SkillVitalType.Special => Vital.Special,
        _ => Vital.HP
    };

    private void SendHealing(Vital vital, int value, IEntity entity, IInstance instance) {
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
        Vital.HP => QbColor.HealingGreen,
        Vital.MP => QbColor.DeepSkyBlue,
        Vital.Special => QbColor.Coral,
        _ => QbColor.HealingGreen
    };

    private bool IsInRange(int range, int x1, int y1, int x2, int y2) {
        var r = Convert.ToInt32(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
        return r <= range;
    }

    private PartyManager? GetPartyManager() {
        var id = Player!.PartyId;
        var parties = InstanceService!.Parties;

        if (parties.ContainsKey(id)) {
            return parties[id];
        }

        return null;
    }
}