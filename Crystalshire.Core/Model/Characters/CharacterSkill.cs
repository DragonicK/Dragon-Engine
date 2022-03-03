using System.ComponentModel.DataAnnotations.Schema;

using Crystalshire.Core.Model.Skills;
using Crystalshire.Core.Model.Passives;

namespace Crystalshire.Core.Model.Characters {
    public sealed class CharacterSkill {
        public long Id { get; set; }
        public long CharacterId { get; set; }
        public int SkillId { get; set; }
        public int SkillLevel { get; set; }
        [NotMapped]
        public SkillTargetType TargetType { get; private set; }
        [NotMapped]
        public SkillAttributeType AttributeType { get; private set; }
        [NotMapped]
        public ElementAttribute Element { get; private set; }
        [NotMapped]
        public float Amplification { get; private set; }
        [NotMapped]
        public int Range { get; private set; }
        [NotMapped]
        public int CastTime { get; private set; }
        [NotMapped]
        public int Cooldown { get; private set; }
        [NotMapped]
        public int StunDuration { get; private set; }
        [NotMapped]
        public int Cost { get; private set; }
        [NotMapped]
        public Dictionary<SkillEffectType, SkillEffect> Effects { get; private set; }

        public CharacterSkill() {
            Effects = new Dictionary<SkillEffectType, SkillEffect>();
        }

        public void Clear() {
            SkillId = 0;
            SkillLevel = 0;

            ClearData();
        }

        public void ClearData() {
            TargetType = SkillTargetType.None;
            Element = ElementAttribute.Neutral;
            Amplification = 0;
            Range = 0;
            CastTime = 0;
            Cooldown = 0;
            StunDuration = 0;
            Cost = 0;

            Effects.Clear();
        }

        public void AllocateData(Skill skill) {
            AttributeType = skill.AttributeType;
            TargetType = skill.TargetType;
            Element = skill.ElementType;
            Amplification = skill.Amplification + ((SkillLevel - 1) * skill.AmplificationPerLevel);
            Range = skill.Range;
            CastTime = skill.CastTime;
            Cooldown = skill.Cooldown;
            StunDuration = skill.StunDuration;
            Cost = skill.Cost;

            foreach (var effect in skill.Effects) {
                if (Effects.ContainsKey(effect.EffectType)) {
                    AddEffect(effect, SkillLevel);
                }
                else {
                    Effects.Add(effect.EffectType, new SkillEffect());
                    AddEffect(effect, SkillLevel);
                } 
            }
        }

        public void AllocateData(Passive passive, int level) {
            TargetType = passive.TargetType;
            Element = passive.Element;
            Amplification += Convert.ToSingle(level * passive.Amplification);
            Range += level * passive.Range;
            CastTime += level * passive.CastTime;
            Cooldown += level * passive.Cooldown;
            StunDuration += level * passive.Stun;
            Cost += level * passive.Cost;

            var effect = passive.Skill;

            if (effect.EffectType > SkillEffectType.None) {
                if (Effects.ContainsKey(effect.EffectType)) {
                    AddEffect(effect, SkillLevel);
                }
                else {
                    Effects.Add(effect.EffectType, new SkillEffect());
                    AddEffect(effect, SkillLevel);
                }
            }
        }

        private void AddEffect(SkillEffect effect, int level) {
            var index = effect.EffectType;

            Effects[index].EffectType = index;
            Effects[index].VitalType = effect.VitalType;
            Effects[index].TargetType = effect.TargetType;
            Effects[index].Damage += level * effect.Damage;
            Effects[index].Duration += level * effect.Duration;
            Effects[index].Interval += level * effect.Interval;
            Effects[index].StunDuration += level * effect.StunDuration;
            Effects[index].DamagePerLevel += level * effect.DamagePerLevel;

            if (effect.EffectId > 0) {
                Effects[index].EffectId = effect.EffectId;
            }
        }
    }
}