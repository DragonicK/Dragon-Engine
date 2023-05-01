using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Core.Model.Classes;

using Dragon.Game.Services;

using System.Text.RegularExpressions;

namespace Dragon.Game.Characters;

public sealed class CharacterValidator : ICharacterValidator {
    public ConfigurationService? Configuration { get; private set; }
    public ContentService? ContentService { get; private set; }

    private readonly Regex Regex = new("[^a-zA-Z0-9]$"); 

    public CharacterValidator(IServiceInjector injector) {
        injector.Inject(this);
    }

    public IClass GetValidClass(int code) {
        var classes = ContentService!.Classes;

        classes.TryGet(code, out var classJob);

        return classJob;
    }

    public int GetClassModel(IClass classJob, Gender gender, int modelIndex) {
        var sprites = (gender == Gender.Male) ? classJob.MaleSprites : classJob.FemaleSprites;

        if (modelIndex <= 0 || modelIndex >= sprites.Length) {
            return 0;
        }

        return sprites[--modelIndex];
    }

    public CharacterValidatorResult ValidateClass(int code) {
        var classes = ContentService!.Classes;

        classes.TryGet(code, out var classJob);

        if (classJob is null) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        if (!classJob.Selectable) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        return new CharacterValidatorResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidatorResult ValidateName(string name) {
        if (string.IsNullOrEmpty(name)) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        var result = Regex.Match(name);

        if (result.Success) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.NameIllegal,
                MenuResetType = MenuResetType.Characters
            };
        }

        var prohibitedNames = Configuration!.ProhibitedNames;

        if (prohibitedNames.IsProhibited(name)) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.NameIllegal,
                MenuResetType = MenuResetType.Characters,
                Disconnect = false
            };
        }

        var character = Configuration.Character;

        if (name.Length < character.MinimumNameLength || name.Length > character.MaximumNameLength) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.NameLength,
                MenuResetType = MenuResetType.Characters,
            };
        }

        return new CharacterValidatorResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidatorResult ValidateGender(int gender) {
        if (!Enum.IsDefined(typeof(Gender), gender)) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        return new CharacterValidatorResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidatorResult ValidateCharacterIndex(int index) {
        var character = Configuration!.Character;

        if (index < 1 || index > character.Maximum) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        return new CharacterValidatorResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }

    public CharacterValidatorResult ValidateModel(IClass classJob, Gender gender, int index) {
        var sprites = (gender == Gender.Male) ? classJob.MaleSprites : classJob.FemaleSprites;

        if (index <= 0 || index >= sprites.Length) {
            return new CharacterValidatorResult() {
                AlertMessageType = AlertMessageType.Connection,
                MenuResetType = MenuResetType.Login,
                Disconnect = true
            };
        }

        return new CharacterValidatorResult() {
            AlertMessageType = AlertMessageType.None,
            MenuResetType = MenuResetType.None
        };
    }
}