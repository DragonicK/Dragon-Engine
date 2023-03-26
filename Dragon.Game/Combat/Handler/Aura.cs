using Dragon.Core.Content;
using Dragon.Core.Model;
using Dragon.Core.Model.Entity;
using Dragon.Core.Model.Skills;
using Dragon.Core.Model.Effects;
using Dragon.Core.Model.Characters;

using Dragon.Game.Manager;
using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Combat.Common;

namespace Dragon.Game.Combat.Handler;

public class Aura : ISkillHandler {
    public IPlayer? Player { get; set; }
    public IDatabase<Effect>? Effects { get; set; }
    public IPacketSender? PacketSender { get; set; }
    public InstanceService? InstanceService { get; set; }

    private int Id;
    private int Level;
    private int Range;
    private Effect? AttributeEffect;

    public bool CanSelect(Target target, SkillEffect effect) {
        return true;
    }

    public Damaged GetDamage(Target target, CharacterSkill inventory, SkillEffectType type) {
        Range = inventory.Range;
        Level = inventory.SkillLevel;

        return new Damaged();
    }

    public IList<Target> GetTarget(Target target, IInstance instance, CharacterSkill inventory, SkillEffect effect) {
        AttributeEffect = Effects![effect.EffectId];

        if (AttributeEffect is not null) {
            Id = AttributeEffect.Id;
        }

        var list = new List<Target>(1) {
                new Target() {
                    Entity = Player!,
                    Type = TargetType.Player
                }
            };

        return list;
    }

    public void Inflict(Damaged damaged, Target target, IInstance instance, SkillEffect effect) {
        if (AttributeEffect is not null) {
            var manager = new AuraManager() {
                Player = Player,
                Effects = Effects,
                PacketSender = PacketSender,
                InstanceService = InstanceService
            };

            if (Player!.Auras.Contains(Id)) {
                Player!.Auras.Remove(Id);

                manager.DeactivateAura(Id);
            }
            else {
                Player!.Auras.Add(Id, Level, Range);

                manager.ActivateAura(Id, Level, Range);
            }
        }
    }
}