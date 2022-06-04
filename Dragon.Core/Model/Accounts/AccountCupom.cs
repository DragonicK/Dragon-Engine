using System;

namespace Dragon.Core.Model.Accounts;

public class AccountCupom {
    public long Id { get; set; }
    public long AccountId { get; set; }
    public long CupomId { get; set; }
    public DateTime RegisteredDate { get; set; }
}