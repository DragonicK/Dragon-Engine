namespace Dragon.Core.Model.Entity;

public interface IEntityVital {
    int Get(Vital vital);
    int GetMaximum(Vital vital);
    void Set(Vital vital, int value);
    void SetMaximum(Vital vital, int value);
    void Add(Vital vital, int value);
}