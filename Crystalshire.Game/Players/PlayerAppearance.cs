using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public class PlayerAppearance : IPlayerAppearance {
        private readonly CharacterAppearance _appearance;

        public PlayerAppearance(long characterId, CharacterAppearance appearance) {
            _appearance = appearance;

            if (_appearance is null) {
                _appearance = new CharacterAppearance() { 
                    CharacterId = characterId
                };
            }
        }
    }
}