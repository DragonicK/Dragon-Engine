using Dragon.Core.Services;

using Dragon.Game.Services;
using Dragon.Game.Instances.Chests;

namespace Dragon.Game.Parties;

public sealed class PartyItem {
    public IInstanceChest? Chest { get; set; }
    public IInstanceChestItem? ChestItem { get; set; }
    public int ItemRollRemainingTime { get; set; }
    public List<PartyCharacterDice> Dices { get; set; }
    public bool IsRollCompleted => IsMembersRollDiceCompleted();
    private ConfigurationService Configuration { get; set; }

    public PartyItem(IServiceInjector injector, IInstanceChestItem chestItem) {
        injector.Inject(this);

        var maximum = Configuration!.Group.MaximumMembers;

        Dices = new List<PartyCharacterDice>(maximum);

        ChestItem = chestItem;
    }

    public long GetWinnerCharacterId() {
        var characterId = 0L;
        var maxValue = 1;

        for (var i = 0; i < Dices.Count; i++) {
            if (Dices[i].RolledNumber > maxValue) {
                maxValue = Dices[i].RolledNumber ;
                characterId = Dices[i].CharacterId;
            }
        }

        return characterId;
    }

    private bool IsMembersRollDiceCompleted() {
        for (var i = 0; i < Dices.Count; i++) {
            if (Dices[i].State == PartyCharacterDiceState.Waiting) {
                return false;
            }
        }

        return true;
    }
}