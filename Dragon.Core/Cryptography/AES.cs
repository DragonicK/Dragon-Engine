using System.Security.Cryptography;

namespace Dragon.Core.Cryptography;

public sealed class AES {
    public int KeySize { get; set; } = 128;
    public CipherMode CipherMode { get; set; }
    public PaddingMode PaddingMode { get; set; }

    public static int KeyLength { get; } = 16;
    public static int BlockSize { get; } = 128;

    public byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] key, byte[] iv) {
        using var ms = new MemoryStream();
        using var aes = Aes.Create();

        aes.KeySize = KeySize;
        aes.BlockSize = BlockSize;
        aes.Mode = CipherMode;
        aes.Padding = PaddingMode;

        aes.Key = key;
        aes.IV = iv;

        using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)) {
            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
            cs.Close();
        }

        return ms.ToArray();
    }

    public byte[]? Decrypt(byte[] bytesToBeDecrypted, byte[] key, byte[] iv) {
        byte[] encryptedBytes;

        try {
            using var ms = new MemoryStream();
            using var aes = Aes.Create();

            aes.KeySize = KeySize;
            aes.BlockSize = BlockSize;
            aes.Mode = CipherMode;
            aes.Padding = PaddingMode;

            aes.Key = key;
            aes.IV = iv;

            using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write)) {
                cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                cs.Close();
            }

            encryptedBytes = ms.ToArray();
        }
        catch {
            return null;
        }

        return encryptedBytes;
    }
}