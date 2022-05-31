namespace Crystalshire.Core.Model;

public enum AuthenticationResult {
    None,
    Success,
    Failed,
    Maintenance,
    WrongUserData,
    AccountIsNotActivated,
    AccountIsBanned,
    VersionOutdated,
    StringLength
}