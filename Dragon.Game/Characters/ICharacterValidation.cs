using Dragon.Core.Model;
using Dragon.Core.Model.Classes;

namespace Dragon.Game.Characters;

public interface ICharacterValidation {
    public string CharacterName { get; }
    public int CharacterIndex { get; }
    public IClass CharacterClass { get; }
    public Gender Gender { get; }
    public int Model { get; }

    CharacterValidationResult ValidateName(string characterName);
    CharacterValidationResult ValidateCharacterIndex(int characterIndex);
    CharacterValidationResult ValidateClass(int classId);
    CharacterValidationResult ValidateGender(int genderType);
    CharacterValidationResult ValidateModel(int modelIndex);
}