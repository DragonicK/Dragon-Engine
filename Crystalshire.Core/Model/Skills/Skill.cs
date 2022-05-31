namespace Crystalshire.Core.Model.Skills;

public class Skill {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Sound { get; set; }
    public int IconId { get; set; }
    public SkillType Type { get; set; }
    public SkillAttributeType AttributeType { get; set; }
    public SkillTargetType TargetType { get; set; }
    public ElementAttribute ElementType { get; set; }
    public SkillCostType CostType { get; set; }
    public SkillEffectType EffectType { get; set; }
    public int MaximumLevel { get; set; }
    public float Amplification { get; set; }
    public float AmplificationPerLevel { get; set; }
    public int Range { get; set; }
    public int Cost { get; set; }
    public int CostPerLevel { get; set; }
    public int CastTime { get; set; }
    public int Cooldown { get; set; }
    public int StunDuration { get; set; }
    public int CastAnimationId { get; set; }
    public int AttackAnimationId { get; set; }
    public int PassiveId { get; set; }
    public IList<SkillEffect> Effects { get; set; }

    public Skill() {
        Name = string.Empty;
        Description = string.Empty;
        Effects = new List<SkillEffect>();
        Sound = "None.";
    }

    public override string ToString() {
        return Name;
    }
}