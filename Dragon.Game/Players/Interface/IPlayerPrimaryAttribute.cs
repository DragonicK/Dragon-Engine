using Dragon.Core.Model;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerPrimaryAttribute {
    CharacterPrimaryAttribute GetPrimaryAttributes();
    void Add(PrimaryAttribute attribute, int value);
    void Set(PrimaryAttribute attribute, int value);
    int Get(PrimaryAttribute attribute);
}