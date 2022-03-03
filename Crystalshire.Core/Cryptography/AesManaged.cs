using System.Security.Cryptography;

namespace Crystalshire.Core.Cryptography {
    public class AesManaged {
        public int KeySize { get; set; } = 128;
        public CipherMode CipherMode { get; set; }
        public PaddingMode PaddingMode { get; set; }

        private const int KeyLength = 16;
        private const int BlockSize = 128;

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

        public byte[] CreateKey(string passphrase) {
            var key = new byte[KeyLength];
            var hash = Hash.Compute(passphrase);

            for (var i = 0; i < KeyLength; ++i) {
                key[i] = (byte)hash[i % hash.Length];
            }

            return key;
        }

        public byte[] CreateIv(string passphrase) {
            var key = new byte[KeyLength];
            var hash = Hash.Compute(passphrase);

            for (var i = KeyLength - 1; i >= 0; --i) {
                key[i] = (byte)hash[i % hash.Length];
            }

            return key;
        }
    }
}