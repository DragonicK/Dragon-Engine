using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public class PlayerCurrency : IPlayerCurrency {
        private readonly IList<CharacterCurrency> _currencies;
        private readonly long _characterId;

        public PlayerCurrency(long characterId, IList<CharacterCurrency> currencies) {
            _currencies = currencies;
            _characterId = characterId; 

            if (_currencies is null) {
                _currencies = new List<CharacterCurrency>();
            }
        }

        public bool Add(CurrencyType currencyType, int value) {
            var currency = GetCurrency(currencyType);

            var r = currency.CurrencyValue + value;

            if (r > int.MaxValue) {
                return false;
            }

            currency.CurrencyValue = r;

            return true;
        }

        public int Get(CurrencyType currencyType) {
            return GetCurrency(currencyType).CurrencyValue;
        }

        public void Round(CurrencyType currencyType, int value) {
            var currency = GetCurrency(currencyType);

            long r = currency.CurrencyValue + value;

            currency.CurrencyValue = r > int.MaxValue ? int.MaxValue : (int)r;
        }

        public void Set(CurrencyType currencyType, int value) {
            GetCurrency(currencyType).CurrencyValue = value;
        }

        public bool Subtract(CurrencyType currencyType, int value) {
            var currency = GetCurrency(currencyType);

            var r = currency.CurrencyValue - value;

            if (r < 0) {
                return false;
            }

            currency.CurrencyValue = r;

            return true;
        }

        public CharacterCurrency GetCurrency(CurrencyType currencyType) {
            var selected = _currencies.FirstOrDefault(x => x.CurrencyId == currencyType);

            if (selected is null) {
                selected = new CharacterCurrency() {
                    CharacterId = _characterId,
                    CurrencyId = currencyType,
                };

                _currencies.Add(selected);
            }

            return selected;
        }

        public IList<CharacterCurrency> ToList() {
            return _currencies;
        }
    }
}