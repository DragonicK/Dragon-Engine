namespace Dragon.Core.Model.Items;

public class Item {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Sound { get; set; }
    public int IconId { get; set; }
    public ItemType Type { get; set; }
    public Rarity Rarity { get; set; }
    public BindType Bind { get; set; }
    public int RequiredLevel { get; set; }
    public int Price { get; set; }
    public int MaximumStack { get; set; }
    public int GashaBoxId { get; set; }
    public int RecipeId { get; set; }
    public int EquipmentId { get; set; }
    public int SkillId { get; set; }

    // Consume, Potions.
    public int Cooldown { get; set; }
    public int EffectId { get; set; }
    public int EffectLevel { get; set; }
    public int EffectDuration { get; set; }
    public int[] Vital { get; set; }
    public int Interval { get; set; }
    public int Duration { get; set; }
    public int ClassCode { get; set; }
    public int UpgradeId { get; set; }
    public int MaximumLevel { get; set; }

    public Item() {
        Sound = "None.";
        Name = string.Empty;
        Description = string.Empty;
        Vital = new int[Enum.GetValues(typeof(Vital)).Length];
    }

    public override string ToString() {
        return Name;
    }
}