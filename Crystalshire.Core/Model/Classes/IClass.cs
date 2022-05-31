using System.Text.Json.Serialization;

namespace Crystalshire.Core.Model.Classes;

[JsonConverter(typeof(IClassConverter))]
public interface IClass {
    int Id { get; set; }
    string Name { get; set; }
    bool Selectable { get; set; }

    int[] MaleSprites { get; set; }
    int[] FemaleSprites { get; set; }
    bool GenderLock { get; set; }
    Gender Gender { get; set; }

    int Level { get; set; }
    int Experience { get; set; }
    int AttributePoint { get; set; }
    int AttributePointPerLevel { get; set; }

    int MapId { get; set; }
    int X { get; set; }
    int Y { get; set; }

    Dictionary<Vital, int> Vital { get; set; }
    Dictionary<PrimaryAttribute, int> Primary { get; set; }
    Dictionary<SecondaryAttribute, int> Secondary { get; set; }
    Dictionary<UniqueAttribute, float> Unique { get; set; }
    Dictionary<ElementAttribute, int> ElementAttack { get; set; }
    Dictionary<ElementAttribute, int> ElementDefense { get; set; }

    Dictionary<Vital, IClassGrowth> VitalGrowth { get; set; }
    Dictionary<SecondaryAttribute, IClassGrowth> SecondaryGrowth { get; set; }
    Dictionary<UniqueAttribute, IClassGrowth> UniqueGrowth { get; set; }

    IList<ClassSkill> Skills { get; set; }
    IList<ClassPassive> Passives { get; set; }
    IList<ClassInventory> Inventories { get; set; }
    IList<ClassEquipment> Equipments { get; set; }
}