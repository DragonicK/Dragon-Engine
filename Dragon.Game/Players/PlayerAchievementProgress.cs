using Dragon.Core.Model.Characters;

namespace Dragon.Game.Players;

public class PlayerAchievementProgress : IPlayerAchievementProgress {
    private readonly IList<CharacterAchievementProgress> _progress;

    public PlayerAchievementProgress(long characterId, IList<CharacterAchievementProgress> progress) {
        _progress = progress;
    }
}