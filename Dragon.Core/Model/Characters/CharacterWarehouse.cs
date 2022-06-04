namespace Dragon.Core.Model.Characters;

public class CharacterWarehouse {
    public long Id { get; set; }
    public long CharacterId { get; set; }
    public int InventoryIndex { get; set; }
    public int ItemId { get; set; }
    public int Value { get; set; }
    public bool Bound { get; set; }
    public int Level { get; set; }
    public int AttributeId { get; set; }
    public int UpgradeId { get; set; }
    public string UniqueSerial { get; set; } = string.Empty;
    public int Charge { get; set; }
    public byte IsPacked { get; set; }
    public byte WrappableCount { get; set; }
    public int FusionedItemId { get; set; }
    public int Socket { get; set; }
    public int FusionedSocket { get; set; }
    public short ActivationCount { get; set; }
    public int ItemSkinId { get; set; }

    public void Apply(CharacterInventory inventory) {
        CharacterId = inventory.CharacterId;
        ItemId = inventory.ItemId;
        Value = 1;
        Bound = inventory.Bound;
        Level = inventory.Level;
        AttributeId = inventory.AttributeId;
        UpgradeId = inventory.UpgradeId;
        UniqueSerial = inventory.UniqueSerial;
        Charge = inventory.Charge;
        IsPacked = inventory.IsPacked;
        WrappableCount = inventory.WrappableCount;
        FusionedItemId = inventory.FusionedItemId;
        Socket = inventory.Socket;
        FusionedSocket = inventory.FusionedSocket;
        ActivationCount = inventory.ActivationCount;
        ItemSkinId = inventory.ItemSkinId;
    }
    public void Clear() {
        ItemId = 0;
        Value = 0;
        Bound = false;
        Level = 0;
        AttributeId = 0;
        UpgradeId = 0;
        UniqueSerial = string.Empty;
        Charge = 0;
        IsPacked = 0;
        WrappableCount = 0;
        FusionedItemId = 0;
        Socket = 0;
        FusionedSocket = 0;
        ActivationCount = 0;
        ItemSkinId = 0;
    }
}