using Dragon.Core.Model;
using Dragon.Core.Model.Skills;

namespace Dragon.Core.Content;

public class Skills : Database<Skill> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var skill = new Skill {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString(),
                    Sound = reader.ReadString(),
                    IconId = reader.ReadInt32(),
                    Type = (SkillType)reader.ReadInt32(),
                    AttributeType = (SkillAttributeType)reader.ReadInt32(),
                    TargetType = (SkillTargetType)reader.ReadInt32(),
                    ElementType = (ElementAttribute)reader.ReadInt32(),
                    CostType = (SkillCostType)reader.ReadInt32(),
                    EffectType = (SkillEffectType)reader.ReadInt32(),
                    MaximumLevel = reader.ReadInt32(),
                    Amplification = reader.ReadSingle(),
                    AmplificationPerLevel = reader.ReadSingle(),
                    Range = reader.ReadInt32(),
                    Cost = reader.ReadInt32(),
                    CostPerLevel = reader.ReadInt32(),
                    CastTime = reader.ReadInt32(),
                    Cooldown = reader.ReadInt32(),
                    StunDuration = reader.ReadInt32(),
                    CastAnimationId = reader.ReadInt32(),
                    AttackAnimationId = reader.ReadInt32(),
                    PassiveId = reader.ReadInt32()
                };

                var effectCount = reader.ReadInt32();

                for (var n = 0; n < effectCount; ++n) {
                    var effect = new SkillEffect() {
                        EffectType = (SkillEffectType)reader.ReadInt32(),
                        VitalType = (SkillVitalType)reader.ReadInt32(),
                        TargetType = (SkillTargetType)reader.ReadInt32(),
                        Direction = (Direction)reader.ReadInt32(),
                        Damage = reader.ReadInt32(),
                        DamagePerLevel = reader.ReadInt32(),
                        Duration = reader.ReadInt32(),
                        Interval = reader.ReadInt32(),
                        StunDuration = reader.ReadInt32(),
                        InstanceId = reader.ReadInt32(),
                        X = reader.ReadInt32(),
                        Y = reader.ReadInt32(),
                        EffectId = reader.ReadInt32(),
                        Trigger = reader.ReadInt32()
                    };

                    skill.Effects.Add(effect);
                }

                values.Add(skill.Id, skill);
            }
        }
    }

    public override void Save() {
        var path = $"{Folder}/{FileName}";

        using var file = new FileStream(path, FileMode.Create, FileAccess.Write);
        using var writer = new BinaryWriter(file);

        writer.Write(values.Count);

        var ordered = values.Select(p => p.Value).OrderBy(p => p.Id).ToList();

        for (var i = 0; i < ordered.Count; ++i) {
            var skill = ordered[i];

            writer.Write(skill.Id);
            writer.Write(skill.Name);
            writer.Write(skill.Description);
            writer.Write(skill.Sound);
            writer.Write(skill.IconId);
            writer.Write((int)skill.Type);
            writer.Write((int)skill.AttributeType);
            writer.Write((int)skill.TargetType);
            writer.Write((int)skill.ElementType);
            writer.Write((int)skill.CostType);
            writer.Write((int)skill.EffectType);
            writer.Write(skill.MaximumLevel);
            writer.Write(skill.Amplification);
            writer.Write(skill.AmplificationPerLevel);
            writer.Write(skill.Range);
            writer.Write(skill.Cost);
            writer.Write(skill.CostPerLevel);

            writer.Write(skill.CastTime);
            writer.Write(skill.Cooldown);
            writer.Write(skill.StunDuration);
            writer.Write(skill.CastAnimationId);
            writer.Write(skill.AttackAnimationId);
            writer.Write(skill.PassiveId);

            var effectCount = skill.Effects.Count;

            writer.Write(effectCount);

            for (var n = 0; n < effectCount; ++n) {
                var effect = skill.Effects[n];

                writer.Write((int)effect.EffectType);
                writer.Write((int)effect.VitalType);
                writer.Write((int)effect.TargetType);
                writer.Write((int)effect.Direction);
                writer.Write(effect.Damage);
                writer.Write(effect.DamagePerLevel);
                writer.Write(effect.Duration);
                writer.Write(effect.Interval);
                writer.Write(effect.StunDuration);
                writer.Write(effect.InstanceId);
                writer.Write(effect.X);
                writer.Write(effect.Y);
                writer.Write(effect.EffectId);
                writer.Write(effect.Trigger);
            }
        }
    }

}