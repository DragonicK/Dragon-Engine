using System.Text.RegularExpressions;
using System.Diagnostics.CodeAnalysis;

using Dragon.Core.Model;
using Dragon.Core.Content;
using Dragon.Core.Model.Classes;

using Dragon.Game.Configurations;

namespace Dragon.Game.Characters;

public class CharacterValidation : ICharacterValidation {
    [AllowNull]
    public IClass? CharacterClass { get; private set; }
    public int Model { get; private set; }
    public Gender Gender { get; private set; }
    public int CharacterIndex { get; private set; }
    public string CharacterName { get; private set; }

    private readonly IConfiguration configuration;
    private readonly IDatabase<IClass> classes;

    public CharacterValidation(IDatabase<IClass> classes, IConfiguration configuration) {
        CharacterName = string.Empty;
        this.classes = classes;
        this.configuration = configuration;
    }

    public CharacterValidationResult ValidateCharacterIndex(int characterIndex) {
        var character = configuration.Character;

        if (characterIndex < 1 || characterIndex > character.Maximum) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        CharacterIndex = characterIndex;

        return new CharacterValidationResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidationResult ValidateClass(int classId) {
        if (classes is null) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        var classe = classes[classId];

        if (classe is null) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        var selectable = classe.Selectable;

        if (!selectable) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        CharacterClass = classe;

        return new CharacterValidationResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidationResult ValidateGender(int genderType) {
        if (!Enum.IsDefined(typeof(Gender), genderType)) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        Gender = (Gender)genderType;

        return new CharacterValidationResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidationResult ValidateName(string characterName) {
        if (string.IsNullOrEmpty(characterName)) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        characterName = characterName.Replace(" ", string.Empty);

        var regex = new Regex("[^a-zA-Z0-9]$");
        var result = regex.Match(characterName);

        if (result.Success) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.NameIllegal,
                MenuResetType = MenuResetType.Characters
            };
        }

        var prohibitedNames = configuration.ProhibitedNames;

        if (prohibitedNames.IsProhibited(characterName)) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.NameIllegal,
                MenuResetType = MenuResetType.Characters,
                Disconnect = false
            };
        }

        var character = configuration.Character;

        if (characterName.Length < character.MinimumNameLength || characterName.Length > character.MaximumNameLength) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.NameLength,
                MenuResetType = MenuResetType.Characters,
            };
        }

        CharacterName = characterName;

        return new CharacterValidationResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidationResult ValidateModel(int modelIndex) {
        int[]? models = null;

        if (Gender == Gender.Male) {
            models = CharacterClass.MaleSprites;
        }

        if (Gender == Gender.Female) {
            models = CharacterClass.FemaleSprites;
        }

        if (modelIndex < 0 || modelIndex >= models!.Length) {
            return new CharacterValidationResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        // Client model index starts at 1.
        // So we need to decrease.
        modelIndex--;

        Model = models[modelIndex];

        return new CharacterValidationResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }
}