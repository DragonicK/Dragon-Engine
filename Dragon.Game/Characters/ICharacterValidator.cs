using Dragon.Core.Model;
using Dragon.Core.Model.Classes;

namespace Dragon.Game.Characters;

public interface ICharacterValidator {
    IClass GetValidClass(int code);
    CharacterValidatorResult ValidateClass(int code);
    CharacterValidatorResult ValidateName(string name);
    CharacterValidatorResult ValidateGender(int gender);
    CharacterValidatorResult ValidateCharacterIndex(int index);
    int GetClassModel(IClass classJob, Gender gender, int modelIndex);
    CharacterValidatorResult ValidateModel(IClass classJob, Gender gender, int index);
}