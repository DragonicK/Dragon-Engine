namespace Dragon.Core.Model.Equipments;

public class Equipment {
    public int Id { get; set; }
    public string Name { get; set; }
    public EquipmentType Type { get; set; }
    public EquipmentHandStyle HandStyle { get; set; }
    public EquipmentProficiency Proficiency { get; set; }
    public Rarity Rarity { get; set; }
    public int Level { get; set; }
    public int ModelId { get; set; }
    public int UpgradeId { get; set; }
    public int EquipmentSetId { get; set; }
    public int DisassembleId { get; set; }
    public int MaximumSocket { get; set; }
    public int BaseAttackSpeed { get; set; }
    public int AttackAnimationId { get; set; }
    public IList<EquipmentSkill> Skills { get; set; }
    public IList<EquipmentAttribute> Attributes { get; set; }

    public Equipment() {
        Name = string.Empty;
        Skills = new List<EquipmentSkill>();
        Attributes = new List<EquipmentAttribute>();
    }

    public override string ToString() {
        return Name;
    }
}