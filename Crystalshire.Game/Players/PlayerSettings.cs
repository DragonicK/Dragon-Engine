using Crystalshire.Core.Model.Characters;

namespace Crystalshire.Game.Players {
    public class PlayerSettings : IPlayerSettings {
        private readonly CharacterSettings _settings;

        public PlayerSettings(long characterId, CharacterSettings settings) {
            _settings = settings;

            if (_settings is null) {
                _settings = new CharacterSettings() {
                    CharacterId = characterId
                };
            }
        }
    }
}