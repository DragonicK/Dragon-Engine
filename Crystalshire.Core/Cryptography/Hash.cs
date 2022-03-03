using System.Text;
using System.Security.Cryptography;

namespace Crystalshire.Core.Cryptography {
    public static class Hash {

        public static byte[] Compute(string data) {
            var sha = SHA256.Create();
            return sha.ComputeHash(Encoding.Unicode.GetBytes(data));
        }


        public static string ComputeToHex(string data) {
            var sha = SHA256.Create();
            var buffer = sha.ComputeHash(Encoding.Unicode.GetBytes(data));

            var hash = new StringBuilder(buffer.Length * 2);

            sha.Dispose();

            foreach (var bytes in buffer) {
                hash.Append(bytes.ToString("x2"));
            }

            return hash.ToString();
        }

        public static byte[] Compute(byte[] data, int length, bool reverse) {
            var hash = new byte[length];
            var copy = new byte[data.Length];

            Array.Copy(data, 0, copy, 0, data.Length);

            if (reverse) {
                Array.Reverse(copy);
            }

            var sha = SHA256.Create();
            var buffer = sha.ComputeHash(copy);

            sha.Dispose();

            Array.Copy(buffer, 0, hash, 0, length);

            return hash;
        }
    }
}