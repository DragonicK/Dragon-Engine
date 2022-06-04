using Dragon.Core.Model;

namespace Dragon.Game.Characters;

public struct CharacterValidationResult {
    public AlertMessageType AlertMessageType { get; set; }
    public MenuResetType MenuResetType { get; set; }
    public bool Disconnect { get; set; }
}