namespace Crystalshire.Core.Model.Characters {
    public class CharacterMailAttachItem {
        public long Id { get; set; }
        public long MailId { get; set; }
        public int ItemId { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public bool Bound { get; set; }
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
    }
}