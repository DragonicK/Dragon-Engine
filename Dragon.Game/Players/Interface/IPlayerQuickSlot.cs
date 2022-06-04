using Dragon.Core.Model;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerQuickSlot {
    void Change(int index, QuickSlotType type, int value);
    void Swap(int source, int destination);
    CharacterQuickSlot? Get(int index);
    IList<CharacterQuickSlot> ToList();
}