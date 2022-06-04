namespace Dragon.Core.Model.Entity;

public class EntityVital : IEntityVital {
    private readonly int[] current;
    private readonly int[] maximum;

    public EntityVital() {
        var length = Enum.GetValues<Vital>().Length;

        current = new int[length];
        maximum = new int[length];
    }

    public int Get(Vital vital) {
        return current[(int)vital];
    }

    public int GetMaximum(Vital vital) {
        return maximum[(int)vital];
    }

    public void Set(Vital vital, int value) {
        current[(int)vital] = value;
    }

    public void SetMaximum(Vital vital, int value) {
        if (Get(vital) == maximum[(int)vital]) {
            Set(vital, value);
        }

        maximum[(int)vital] = value;
    }

    public void Add(Vital vital, int value) {
        value = Get(vital) + value;

        if (value > GetMaximum(vital)) {
            value = GetMaximum(vital);
        }

        Set(vital, value);
    }
}