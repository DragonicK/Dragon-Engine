namespace Crystalshire.Core.Model.Accounts;

public class AccountLock {
    public long Id { get; set; }
    public long AccountId { get; set; }
    public bool Permanent { get; set; }
    public bool Expired { get; set; }
    public DateTime ExpireDate { get; set; }
}