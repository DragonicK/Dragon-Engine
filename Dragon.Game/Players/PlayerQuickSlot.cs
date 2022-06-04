using Dragon.Core.Model;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public class PlayerQuickSlot : IPlayerQuickSlot {
    private readonly IList<CharacterQuickSlot> _slots;
    private readonly long _characterId;

    public PlayerQuickSlot(long characterId, IList<CharacterQuickSlot> slots) {
        _slots = slots;
        _characterId = characterId;
    }

    public void Change(int index, QuickSlotType type, int value) {
        var slot = Get(index);

        if (slot is null) {
            slot = new CharacterQuickSlot() {
                CharacterId = _characterId,
            };

            _slots.Add(slot);
        }

        slot.QuickSlotIndex = index;
        slot.ObjectType = type;
        slot.ObjectValue = value;
    }

    public void Swap(int source, int destination) {
        var _source = Get(source);
        var _destination = Get(destination);

        if (_source is not null) {
            if (_destination is null) {
                _destination = new CharacterQuickSlot() {
                    CharacterId = _characterId
                };

                _slots.Add(_destination);
            }

            _source.QuickSlotIndex = destination;
            _destination.QuickSlotIndex = source;
        }
    }

    public CharacterQuickSlot? Get(int index) {
        return _slots.FirstOrDefault(p => p.QuickSlotIndex == index);
    }

    public IList<CharacterQuickSlot> ToList() {
        return _slots;
    }
}