using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Services;
using Dragon.Core.Model.Npcs;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Characters;
using Dragon.Core.Model.DisplayIcon;

using Dragon.Game.Parties;
using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Combat.Common;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Combat.Handler;

public sealed class Buff : ISkillHandler {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private int Id;
    private int Level;
    private int Duration;
    private Effect? AttributeEffect;

    private readonly List<Target> Targets;
    private readonly IPlayer Player;

    public Buff(IServiceInjector injector, IPlayer player) {
        injector.Inject(this);

        Player = player;

        Targets = new List<Target>();
    }

    public bool CanSelect(ref Target target, SkillEffect effect) {
        AttributeEffect = GetDatabaseEffect()[effect.EffectId];

        if (AttributeEffect is not null) {
            Id = AttributeEffect.Id;
            Duration = AttributeEffect.Duration;

            return CouldApplyEffect(target.Entity);
        }

        return false;
    }

    public Damaged GetDamage(ref Target target, CharacterSkill inventory, SkillEffectType type) {
        Level = inventory.SkillLevel;

        return new Damaged();
    }

    public IList<Target> GetTarget(ref Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect) {
        var targetType = effect.TargetType;
        var range = inventory.Range;

        switch (targetType) {
            case SkillTargetType.Caster:
                if (CouldApplyEffect(Player!)) {
                    Targets.Add(new Target() {
                        Entity = Player
                    });
                }

                break;
            case SkillTargetType.Single:
                if (CanSelect(ref target, effect)) {
                    Targets.Add(target);
                }
                else {
                    Targets.Add(new Target() {
                        Entity = Player,
                        Type = TargetType.Player
                    });
                }

                break;
            case SkillTargetType.AoE:
                if (CanSelect(ref target, effect)) {
                    Targets.Add(target);

                    SetAoETarget(instance, target, range);
                }

                break;
            case SkillTargetType.Group:
                Targets.Add(new Target() {
                    Entity = Player!,
                    Type = TargetType.Player
                });

                SetGroupTarget(range);

                break;
        }

        return Targets;
    }

    public void Inflict(ref Damaged damaged, ref Target target, IInstance instance, SkillEffect effect) {
        if (target.Entity is not null) {
            var database = GetDatabaseEffect();
            var entity = target.Entity;

            if (Id > 0) {
                var overridable = entity.Effects.GetOverridable(database[Id]!);

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

    private void SetAoETarget(IInstance instance, Target primary, int range) {
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
                                Targets.Add(new Target() {
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
                    if (CouldApplyEffect(entity)) {
                        if (!entity.IsDead) {
                            x2 = entity.X;
                            y2 = entity.Y;

                            if (entity.Behaviour != NpcBehaviour.Monster && entity.Behaviour != NpcBehaviour.Boss) {
                                if (IsInRange(range, x1, y1, x2, y2)) {
                                    Targets.Add(new Target() {
                                        Entity = entity,
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

    private void SetGroupTarget(int range) {
        if (AttributeEffect is not null) {
            if (AttributeEffect.EffectType == EffectType.Increase) {
                var party = GetPartyManager();

                if (party is not null) {
                    var members = party.Members;

                    foreach (var member in members) {
                        AddGroupMember(range, member);
                    }
                }
            }
        }    
    }

    private void AddGroupMember(int range, PartyMember member) {
        var x1 = Player.Character.X;
        var y1 = Player.Character.Y;

        if (!member.Disconnected) {
            if (member.Player is not null) {
                if (member.Player != Player) {
                    var x2 = member.Player.Character.X;
                    var y2 = member.Player.Character.Y;

                    if (IsInRange(range, x1, y1, x2, y2)) {
                        Targets.Add(new Target() {
                            Entity = member.Player,
                            Type = TargetType.Player
                        });
                    }
                }
            }
        }
    }

    private bool IsInRange(int range, int x1, int y1, int x2, int y2) {
        return Convert.ToInt32(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2))) <= range;
    }

    private PartyManager? GetPartyManager() {
        var id = Player.PartyId;
        var parties = InstanceService!.Parties;

        parties.TryGetValue(id, out var party);

        return party;
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

            GetPacketSender().SendDisplayIcon(ref icon, target, entity, instance);
        }
    }

    private void SendAttributes(IEntity entity, IInstance instance) {
        var sender = GetPacketSender();

        if (entity is IPlayer player) {
            player.AllocateAttributes();

            sender.SendAttributes(player);

            if (instance is not null) {
                sender.SendPlayerVital(player, instance);
            }
            else {
                sender.SendPlayerVital(player);
            }
        }
        else if (entity is IInstanceEntity) {
            entity.Vitals.SetMaximum(Vital.HP, entity.Attributes.Get(Vital.HP));
            entity.Vitals.SetMaximum(Vital.MP, entity.Attributes.Get(Vital.MP));
            entity.Vitals.SetMaximum(Vital.Special, entity.Attributes.Get(Vital.Special));

            sender.SendInstanceEntityVital(instance, entity.IndexOnInstance);
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

    public void ResetTargets() {
        Targets.Clear();
    }

    private IDatabase<Effect> GetDatabaseEffect() {
        return ContentService!.Effects;
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}