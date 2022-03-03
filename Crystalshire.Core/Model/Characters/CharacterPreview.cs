namespace Crystalshire.Core.Model.Characters {
    public class CharacterPreview {
        public long CharacterId { get; set; }
        public string Name { get; set; }
        public short ClassCode { get; set; }
        public short Gender { get; set; }
        public int Model { get; set; }
        public int Level { get; set; }
        public CharacterDeleteRequest? DeleteRequest { get; set; }

        public CharacterPreview(Character character, CharacterDeleteRequest request) {
            CharacterId = character.CharacterId;
            Name = character.Name;
            ClassCode = character.ClassCode;
            Gender = character.Gender;
            Model = character.Model;
            Level = character.Level;

            if (request is not null) {
                DeleteRequest = request;
            }
        }
    }
}