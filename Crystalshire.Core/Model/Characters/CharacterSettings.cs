namespace Crystalshire.Core.Model.Characters {
    public class CharacterSettings {
        public long Id { get; set; }
        public long CharacterId { get; set; }
        public short SettingsType { get; set; }
        public byte[] SettingsValue { get; set; }
    }
}