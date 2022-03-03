using Crystalshire.Core.Model.Skills;

namespace Crystalshire.Core.Model.Passives {
    public class Passive {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PassiveType PassiveType { get; set; }
        public SkillTargetType TargetType { get; set; }
        public ElementAttribute Element { get; set; }
        public int AttributeId { get; set; }
        public int UpgradeId { get; set; }
        public int SkillId { get; set; }
        public float Amplification { get; set; }
        public int Range { get; set; }
        public int CastTime { get; set; }
        public int Cooldown { get; set; }
        public int Stun { get; set; }
        public int Cost { get; set; }
        public SkillEffect Skill { get; set; }
        public PassiveEffectChange EffectChange { get; set; }
        public PassiveActivation Activation { get; set; }
        public PassiveConditional Conditional { get; set; }
        public PassiveActivationResult ActivationResult { get; set; }
        public int ActivationChance { get; set; }

        public Passive() {
            Name = string.Empty;
            Description = string.Empty;
            Skill = new SkillEffect();
        }

        public override string ToString() {
            return Name;
        }
    }
}
