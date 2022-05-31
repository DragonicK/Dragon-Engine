namespace Crystalshire.Core.Model.Attributes;

public class GroupAttribute {
    public static GroupAttribute Empty {
        get {
            if (_empty is null) {
                _empty = new GroupAttribute();
            }

            return _empty;
        }
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Dictionary<Vital, SingleAttribute> Vital { get; set; }
    public Dictionary<PrimaryAttribute, SingleAttribute> Primary { get; set; }
    public Dictionary<SecondaryAttribute, SingleAttribute> Secondary { get; set; }
    public Dictionary<UniqueAttribute, float> Unique { get; set; }
    public Dictionary<ElementAttribute, SingleAttribute> ElementAttack { get; set; }
    public Dictionary<ElementAttribute, SingleAttribute> ElementDefense { get; set; }

    private static GroupAttribute? _empty;

    public GroupAttribute() {
        Name = string.Empty;
        Description = string.Empty;

        Vital = new Dictionary<Vital, SingleAttribute>();
        Primary = new Dictionary<PrimaryAttribute, SingleAttribute>();
        Secondary = new Dictionary<SecondaryAttribute, SingleAttribute>();
        Unique = new Dictionary<UniqueAttribute, float>();
        ElementAttack = new Dictionary<ElementAttribute, SingleAttribute>();
        ElementDefense = new Dictionary<ElementAttribute, SingleAttribute>();

        var vitals = Enum.GetValues<Vital>();
        foreach (var index in vitals) {
            Vital[index] = default;
        }

        var primary = Enum.GetValues<PrimaryAttribute>();
        foreach (var index in primary) {
            Primary[index] = default;
        }

        var secondary = Enum.GetValues<SecondaryAttribute>();
        foreach (var index in secondary) {
            Secondary[index] = default;
        }

        var unique = Enum.GetValues<UniqueAttribute>();
        foreach (var index in unique) {
            Unique[index] = 0;
        }

        var element = Enum.GetValues<ElementAttribute>();
        foreach (var index in element) {
            ElementAttack[index] = default;
            ElementDefense[index] = default;
        }
    }

    public override string ToString() {
        return Name;
    }
}