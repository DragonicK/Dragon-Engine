﻿namespace Dragon.Core.Model;

public enum AlertMessageType {
    None,
    Failed,
    Connection,
    AccountIsBanned,
    Kicked,
    VersionOutdated,
    StringLength,
    IllegalName,
    Maintenance,
    NameTaken,
    NameLength,
    NameIllegal,
    Database,
    WrongAccountData,
    AccountIsNotActivated,
    CharacterDelete,
    CharacterCreation,
    CharacterIsDeleted,
    InvalidLevelDelete,
    DeleteRange,
    DuplicatedLogin,
    TryingToLogin,
    InvalidPacket,
    InvalidRecipientName,
    InvalidItem,
    NotEnoughCash,
    SuccessPurchase,
    NotEnoughCurrency,
}