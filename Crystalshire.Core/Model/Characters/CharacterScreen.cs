namespace Crystalshire.Core.Model.Characters {
    public class CharacterScreen {
        public long CharacterId { get; set; }
        public long AccountId { get; set; }
        public short CharacterIndex { get; set; }
        public string Name { get; set; }
        public short ClassCode { get; set; }
        public short Gender { get; set; }
        public int Sprite { get; set; }
        public int Level { get; set; }
        public int Map { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public bool PendingExclusion { get; set; }
        public DateTime ExclusionDate { get; set; }
        public string ExclusionIpAddress { get; set; }
        public bool IsDeleted { get; set; }

        public CharacterScreen() {
            Name = string.Empty;
            ExclusionIpAddress = string.Empty;
        }
    }
}