using Dragon.Core.Model.Classes;
using Dragon.Core.Model.Attributes;

namespace Dragon.Core.Model.Entity;

public interface IEntityAttribute {
    int Get(Vital vital);
    float GetPercentage(Vital vital);

    int Get(PrimaryAttribute attribute);
    float GetPercentage(PrimaryAttribute attribute);

    int Get(SecondaryAttribute attribute);
    float GetPercentage(SecondaryAttribute attribute);

    int GetElementAttack(ElementAttribute attribute);
    float GetElementAttackPercentage(ElementAttribute attribute);
    int GetElementDefense(ElementAttribute attribute);
    float GetElementDefensePercentage(ElementAttribute attribute);

    float Get(UniqueAttribute attribute);

    void Add(Vital vital, int value);
    void AddPercentage(Vital vital, float value);
    void Add(PrimaryAttribute attribute, int value);
    void AddPercentage(PrimaryAttribute attribute, float value);
    void Add(SecondaryAttribute attribute, int value);
    void AddPercentage(SecondaryAttribute attribute, float value);

    void AddElementAttack(ElementAttribute attribute, int value);
    void AddElementAttackPercentage(ElementAttribute attribute, float value);
    void AddElementDefense(ElementAttribute attribute, int value);
    void AddElementDefensePercentage(ElementAttribute attribute, float value);

    void Add(UniqueAttribute attribute, float value);

    void Clear();
    void Calculate();
    void Calculate(int playerLevel, IClass _class);
    void Add(IEntityAttribute attribute);
    void Add(int level, GroupAttribute attribute, GroupAttribute upgrade);
    void Subtract(int level, GroupAttribute attribute, GroupAttribute upgrade);
}