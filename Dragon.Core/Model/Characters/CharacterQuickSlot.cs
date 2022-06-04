namespace Dragon.Core.Model.Characters;

public class CharacterQuickSlot {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int QuickSlotId { get; set; }
    public int QuickSlotIndex { get; set; }
    public int ObjectValue { get; set; }
    public QuickSlotType ObjectType { get; set; }
}