namespace Dragon.Core.Model.Mailing;

public enum MailingOperationCode {
    Sended,
    Deleted,
    Invalid,
    InvalidItem,
    InvalidReceiver,
    ItemNotReceived,
    CurrencyNotReceived,
    CurrencyIsNotEnough,
    AttachedNotReceived
}