namespace Crystalshire.Core.Smtp {
    public class SmtpConfiguration {
        public bool Enabled { get; set; }
        public string Username { get; set; }
        public string Passphrase { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public bool EnableSsl { get; set; }

        public SmtpConfiguration() {
            Host = "0.0.0.0";
            Username = "default";
            Passphrase = "default";
        }
    }
}