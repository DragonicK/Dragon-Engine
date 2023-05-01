using Dragon.Core.Model;

namespace Dragon.Game.Characters;

public struct CharacterValidatorResult {
    public AlertMessageType AlertMessageType { get; set; }
    public MenuResetType MenuResetType { get; set; }
    public bool Disconnect { get; set; }
}