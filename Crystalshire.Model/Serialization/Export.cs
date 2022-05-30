using Crystalshire.Core.Cryptography;

namespace Crystalshire.Model.Serialization;

public class Export : Serializer {
    public string Passphrase { get; set; } = string.Empty;

    protected override byte[] GetBytes(Bitmap image) {
        var buffer = base.GetBytes(image);

        var aes = new AesManaged() {
            CipherMode = System.Security.Cryptography.CipherMode.CBC,
            PaddingMode = System.Security.Cryptography.PaddingMode.PKCS7,
            KeySize = 128,
        };

        var key = aes.CreateKey(Passphrase);
        var iv = aes.CreateIv(Passphrase);

        return aes.Encrypt(buffer, key, iv);
    }
}