﻿using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public interface IPlayerQuickSlot {
        void Change(int index, QuickSlotType type, int value);
        void Swap(int source, int destination);
        CharacterQuickSlot? Get(int index);
        IList<CharacterQuickSlot> ToList();
    }
}