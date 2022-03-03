using Crystalshire.Core.Content;
using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.Skills;
using Crystalshire.Core.Model.Effects;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Combat.Handler {
    public class Aura : ISkill {
        public IPlayer? Player { get; set; }
        public IDatabase<Effect>? Effects { get; set; }
        public IPacketSender? PacketSender { get; set; }
        public InstanceService? InstanceService { get; set; }

        private int Id;
        private int Level;
        private int Range;
        private Effect? AttributeEffect;

        public bool CouldSelect(Target target, SkillEffect effect) {
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
                    Entity = (IEntity)Player!,
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
}