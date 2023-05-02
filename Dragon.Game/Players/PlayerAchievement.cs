using Dragon.Core.Model.Characters;
using Dragon.Core.Model.Entity;

namespace Dragon.Game.Players;

public sealed class PlayerAchievement : IPlayerAchievement {
    public IEntityAttribute Attributes { get; }

    private readonly IList<CharacterAchievement> _achievements;

    public PlayerAchievement(long characterId, IList<CharacterAchievement> achievements) {
        _achievements = achievements;
        Attributes = new EntityAttribute();
    }
}