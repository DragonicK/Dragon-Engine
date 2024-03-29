﻿using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerSkill {
    int Count { get; }
    bool Contains(int id);
    CharacterSkill Add(int id, int level);
    CharacterSkill? Get(int index);
    IList<CharacterSkill> ToList();
}