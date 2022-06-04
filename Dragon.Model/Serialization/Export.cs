using Dragon.Core.Cryptography;

namespace Dragon.Model.Serialization;

public class Export : Serializer {
    public string Passphrase { get; set; } = string.Empty;

    protected override byte[] GetBytes(Bitmap image) {
        var buffer = base.GetBytes(image);

        var aes = new AES() {
            CipherMode = System.Security.Cryptography.CipherMode.CBC,
            PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7
        };

        var hash = Hash.Compute(Passphrase);
        var key = Hash.Compute(hash, AES.KeyLength, true);
        var iv = Hash.Compute(hash, AES.KeyLength, false);

        return aes.Encrypt(buffer, key, iv);
    }
}