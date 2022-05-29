namespace Crystalshire.Core.Model.Accounts {
    public class AccountAuthentication {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string AuthPassphrase { get; set; }
        public int AuthCode { get; set; }
        public int AuthLockFlag { get; set; }
        public int AuthFailCount { get; set; }
        public DateTime? RegistrationDate { get; set; }

        public AccountAuthentication() {
            AuthPassphrase = string.Empty;
        }
    }
}