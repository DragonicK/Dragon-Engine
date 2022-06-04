namespace Dragon.Core.Model.Maps;

public class Passphrase {
    public string HexPassword { get; set; }
    public byte[] Key { get; set; }
    public byte[] Iv { get; set; }

    public Passphrase() {
        HexPassword = string.Empty;
        Key = Array.Empty<byte>();
        Iv = Array.Empty<byte>();
    }

    public Passphrase(byte[] key, byte[] iv) {
        Key = key;
        Iv = iv;

        HexPassword = string.Empty;
    }
}