namespace Crystalshire.Core.Model.Maps {
    public class Passphrase {
        public string HexPassword { get; set; }
        public byte[] Key { get; set; }
        public byte[] Iv { get; set; }

        public Passphrase() {
            HexPassword = string.Empty;
        }
    }
}