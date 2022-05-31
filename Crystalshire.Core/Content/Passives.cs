using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Skills;
using Crystalshire.Core.Model.Passives;

namespace Crystalshire.Core.Content;

public class Passives : Database<Passive> {

    public override void Load() {
        var path = $"{Folder}/{FileName}";

        if (File.Exists(path)) {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new BinaryReader(file);

            var count = reader.ReadInt32();

            for (var i = 0; i < count; i++) {
                var passive = new Passive {
                    Id = reader.ReadInt32(),
                    Name = reader.ReadString(),
                    Description = reader.ReadString(),
                    PassiveType = (PassiveType)reader.ReadInt32(),
                    TargetType = (SkillTargetType)reader.ReadInt32(),
                    Element = (ElementAttribute)reader.ReadInt32(),
                    AttributeId = reader.ReadInt32(),
                    UpgradeId = reader.ReadInt32(),
                    SkillId = reader.ReadInt32(),
                    Amplification = reader.ReadSingle(),
                    Range = reader.ReadInt32(),
                    CastTime = reader.ReadInt32(),
                    Cooldown = reader.ReadInt32(),
                    Stun = reader.ReadInt32(),
                    Cost = reader.ReadInt32(),

                    EffectChange = (PassiveEffectChange)reader.ReadInt32(),
                    Activation = (PassiveActivation)reader.ReadInt32(),
                    Conditional = (PassiveConditional)reader.ReadInt32(),
                    ActivationResult = (PassiveActivationResult)reader.ReadInt32(),
                    ActivationChance = reader.ReadInt32(),

                    Skill = new SkillEffect() {
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
                    }
                };

                Add(passive.Id, passive);
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
            var passive = ordered[i];

            writer.Write(passive.Id);
            writer.Write(passive.Name);
            writer.Write(passive.Description);
            writer.Write((int)passive.PassiveType);
            writer.Write((int)passive.TargetType);
            writer.Write((int)passive.Element);
            writer.Write(passive.AttributeId);
            writer.Write(passive.UpgradeId);
            writer.Write(passive.SkillId);
            writer.Write(passive.Amplification);
            writer.Write(passive.Range);
            writer.Write(passive.CastTime);
            writer.Write(passive.Cooldown);
            writer.Write(passive.Stun);
            writer.Write(passive.Cost);
            writer.Write((int)passive.EffectChange);
            writer.Write((int)passive.Activation);
            writer.Write((int)passive.Conditional);
            writer.Write((int)passive.ActivationResult);
            writer.Write(passive.ActivationChance);

            var effect = passive.Skill;

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
        }
    }

}