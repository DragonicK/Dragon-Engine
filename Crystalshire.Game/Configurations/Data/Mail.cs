namespace Crystalshire.Game.Configurations.Data {
    public class Mail {
        public bool Enabled { get; set; }
        public int MaximumAttachment { get; set; }
        public int MaximumGold { get; set; }
        public int MaximumMailPerDay { get; set; }
        public int MaximumMail { get; set; }
        public int TimeLimitInDays { get; set; }

        public Mail() {
            Enabled = true;
            MaximumAttachment = 1;
            MaximumGold = 0;
            MaximumMail = 20;
            MaximumMailPerDay = 0;
            TimeLimitInDays = 30;
        }
    }
}