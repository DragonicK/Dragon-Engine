using Dragon.Core.Model;

namespace Dragon.Game.Players;

public interface IPlayerPrimaryAttribute {
    void Add(PrimaryAttribute attribute, int value);
    void Set(PrimaryAttribute attribute, int value);
    int Get(PrimaryAttribute attribute);
}