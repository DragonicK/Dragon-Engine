namespace Crystalshire.Core.Model.Effects;

public class Effect {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public EffectType EffectType { get; set; }
    public int IconId { get; set; }
    public int Duration { get; set; }
    public bool Dispellable { get; set; }
    public bool RemoveOnDeath { get; set; }
    public bool Unlimited { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public EffectOverride Override { get; set; }

    public Effect() {
        Name = string.Empty;
        Description = string.Empty;
    }

    public override string ToString() {
        return Name;
    }
}