using Dragon.Core.Content;
using Dragon.Core.Model;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.DisplayIcon;

using Dragon.Game.Parties;
using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Combat.Common;

namespace Dragon.Game.Combat.Handler;

public class Buff : ISkillHandler {
    public IPlayer? Player { get; set; }
    public IDatabase<Skill>? Skills { get; set; }
    public IDatabase<Effect>? Effects { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }

    private int Id;
    private int Level;
    private int Duration;
    private Effect? AttributeEffect;

    public bool CanSelect(Target target, SkillEffect effect) {
        AttributeEffect = Effects![effect.EffectId];

        if (AttributeEffect is not null) {
            Id = AttributeEffect.Id;
            Duration = AttributeEffect.Duration;

            return CouldApplyEffect(target.Entity);
        }

        return false;
    }

    public Damaged GetDamage(Target target, CharacterSkill inventory, SkillEffectType type) {
        Level = inventory.SkillLevel;

        return new Damaged();
    }

    public IList<Target> GetTarget(Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect) {
        var list = new List<Target>();

        var targetType = effect.TargetType;
        var range = inventory.Range;

        switch (targetType) {
            case SkillTargetType.Caster:
                if (CouldApplyEffect(Player!)) {
                    list.Add(new Target() {
                        Entity = Player!
                    });
                }

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

                    SetAoETarget(list, instance, target, range);
                }

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
            var entity = target.Entity;

            if (Id > 0) {
                var overridable = entity!.Effects.GetOverridable(Effects![Id]!);

                if (overridable is not null) {
                    // SendDisplayIcon(DisplayIconOperation.Remove, overridable.EffectId, 0, 0, instance, entity);

                    overridable.EffectId = Id;
                    overridable.EffectLevel = Level;
                    overridable.EffectDuration = Duration;

                    entity.Effects.UpdateAttributes();

                    SendAttributes(entity, instance);

                    SendDisplayIcon(DisplayIconOperation.Update, Id, Level, Duration, instance, entity);
                }
                else {
                    entity!.Effects.Add(Id, Level, Duration);
                    entity!.Effects.UpdateAttributes();

                    SendAttributes(entity, instance);

                    SendDisplayIcon(DisplayIconOperation.Update, Id, Level, Duration, instance, entity);
                }
            }
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
                    if (player != primary.Entity) {
                        if (CouldApplyEffect(player)) {

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
            }

            var entities = instance.Entities;

            foreach (var entity in entities) {
                if (entity != primary.Entity) {
                    if (CouldApplyEffect((IEntity)entity)) {
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
        }
    }

    private void SetGroupTarget(IList<Target> list, int range) {
        if (AttributeEffect is not null) {
            if (AttributeEffect.EffectType == EffectType.Increase) {
                var party = GetPartyManager();

                if (party is not null) {
                    var members = party.Members;

                    foreach (var member in members) {
                        AddGroupMember(list, range, member);
                    }
                }
            }
        }
    }

    private void AddGroupMember(IList<Target> list, int range, PartyMember member) {
        var x1 = Player!.Character.X;
        var y1 = Player!.Character.Y;

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

    private void SendDisplayIcon(DisplayIconOperation operation, int id, int level, int duration, IInstance? instance, IEntity entity) {
        if (instance is not null) {
            var icon = new DisplayIcon() {
                Id = id,
                Level = level,
                Duration = duration,
                Type = DisplayIconType.Effect,
                DurationType = DisplayIconDuration.Limited,
                ExhibitionType = DisplayIconExhibition.Player,
                SkillType = DisplayIconSkill.None,
                OperationType = operation
            };

            var target = entity is IPlayer ? DisplayIconTarget.Player : DisplayIconTarget.Npc;

            PacketSender!.SendDisplayIcon(ref icon, target, entity, instance);
        }
    }

    private void SendAttributes(IEntity entity, IInstance instance) {
        if (entity is IPlayer player) {
            player!.AllocateAttributes();
            PacketSender!.SendAttributes(player);

            if (instance is not null) {
                PacketSender!.SendPlayerVital(player, instance);
            }
            else {
                PacketSender!.SendPlayerVital(player);
            }
        }
        else if (entity is IInstanceEntity) {
            entity.Vitals.SetMaximum(Vital.HP, entity.Attributes.Get(Vital.HP));
            entity.Vitals.SetMaximum(Vital.MP, entity.Attributes.Get(Vital.MP));
            entity.Vitals.SetMaximum(Vital.Special, entity.Attributes.Get(Vital.Special));

            PacketSender!.SendInstanceEntityVital(instance, entity.IndexOnInstance);
        }
    }

    public bool CouldApplyEffect(IEntity? target) {
        if (AttributeEffect is not null && target is not null) {

            if (AttributeEffect.EffectType == EffectType.Increase) {
                if (target is IInstanceEntity entity) {
                    if (entity is not null) {
                        return entity.Behaviour != NpcBehaviour.Monster && entity.Behaviour != NpcBehaviour.Boss;
                    }
                }

                if (target is IPlayer) {
                    return true;
                }
            }

            if (AttributeEffect.EffectType == EffectType.Decrease) {
                if (target is IInstanceEntity entity) {
                    if (entity is not null) {
                        return entity.Behaviour == NpcBehaviour.Monster || entity.Behaviour == NpcBehaviour.Boss;
                    }
                }
            }

        }

        return false;
    }
}