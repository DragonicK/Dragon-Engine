﻿using Dragon.Core.Model;
using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public interface IPlayerCurrency {
    bool Add(CurrencyType currencyType, int value);
    int Get(CurrencyType currencyType);
    void Round(CurrencyType currencyType, int value);
    void Set(CurrencyType currencyType, int value);
    bool Subtract(CurrencyType currencyType, int value);
    CharacterCurrency GetCurrency(CurrencyType currencyType);
    IList<CharacterCurrency> ToList();
}