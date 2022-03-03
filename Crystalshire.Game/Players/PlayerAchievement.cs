using Crystalshire.Core.Model.Characters;
using Crystalshire.Core.Model.Entity;

namespace Crystalshire.Game.Players {
    public class PlayerAchievement : IPlayerAchievement {
        public IEntityAttribute Attributes { get; }

        private readonly IList<CharacterAchievement> _achievements;

        public PlayerAchievement(long characterId, IList<CharacterAchievement> achievements) {
            _achievements = achievements;
            Attributes = new EntityAttribute();
        }
    }
}