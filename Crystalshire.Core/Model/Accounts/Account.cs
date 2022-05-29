namespace Crystalshire.Core.Model.Accounts {
    public class Account {
        public long AccountId { get; set; }
        public string Username { get; set; }
        public string Passphrase { get; set; }
        public string Email { get; set; }
        public int Cash { get; set; }
        public byte ActivatedCode { get; set; }
        public string AccountKey { get; set; }
        public byte LoggedIn { get; set; }
        public byte AccountLevelCode { get; set; }
        public string LastLoginIp { get; set; }
        public string CurrentIp { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastLogoutDate { get; set; }
        public DateTime? NewbieRewardDate { get; set; }
        public byte NewbieRewardFlag { get; set; }
        public byte ReturnRewardFlag { get; set; }
        public int RewardPoints { get; set; }

        public AccountAuthentication? AccountAuthentication { get; set; }
        public List<AccountCupom>? AccountCupom { get; set; }
        public List<AccountLock>? AccountLock { get; set; }
        public List<AccountService>? AccountService { get; set; }

        public Account() {
            Username = string.Empty;
            Passphrase = string.Empty;
            Email = string.Empty;
            AccountKey = string.Empty;
            LastLoginIp = string.Empty;
            CurrentIp = string.Empty;
        }
    }
}