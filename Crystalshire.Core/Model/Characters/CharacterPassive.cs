namespace Crystalshire.Core.Model.Characters {
    public class CharacterPassive {
        public long Id { get; set; }
        public long CharacterId { get; set; }
        public int PassiveId { get; set; }
        public int PassiveLevel { get; set; }
    }
}