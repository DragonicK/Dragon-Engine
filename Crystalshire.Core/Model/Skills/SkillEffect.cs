namespace Crystalshire.Core.Model.Skills {
    public class SkillEffect {
        public SkillEffectType EffectType { get; set; }
        public SkillVitalType VitalType { get; set; }
        public SkillTargetType TargetType { get; set; }
        public Direction Direction { get; set; }
        public int Damage { get; set; }
        public int DamagePerLevel { get; set; }
        public int Duration { get; set; }
        public int Interval { get; set; }
        public int StunDuration { get; set; }
        public int InstanceId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int EffectId { get; set; }
        public int Trigger { get; set; }
    }
}