namespace Crystalshire.Core.Model.Accounts;

public class AccountService {
    public long Id { get; set; }
    public long AccountId { get; set; }
    public int ServiceId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool Expired { get; set; }
}