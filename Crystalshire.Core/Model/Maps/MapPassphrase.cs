using Crystalshire.Core.Cryptography;

namespace Crystalshire.Core.Model.Maps {
    public class MapPassphrase {
        public const int Hexadecimal = 16;
        public const int KeyLength = 16;
        public const int IvLength = 16;

        public Dictionary<int, string> Passphrases { get; set; }

        private readonly Dictionary<int, Passphrase> calculated;

        public MapPassphrase() {
            calculated = new Dictionary<int, Passphrase>();
            Passphrases = new Dictionary<int, string>();
        }

        public void Add(int instanceId, string passphrase) {
            Passphrases.Add(instanceId, passphrase);
        }

        public void Clear() {
            calculated.Clear();
            Passphrases.Clear();
        }

        public byte[] GetKey(int instanceId) {
            if (!calculated.ContainsKey(instanceId)) {
                AddPassphrase(instanceId);
            }

            return calculated[instanceId].Key;
        }

        public byte[] GetIv(int instanceId) {
            if (!calculated.ContainsKey(instanceId)) {
                AddPassphrase(instanceId);
            }

            return calculated[instanceId].Iv;
        }

        private void AddPassphrase(int instanceId) {
            var bytes = GetBytes(instanceId);
            var key = Hash.Compute(bytes, KeyLength, true);
            var iv = Hash.Compute(bytes, KeyLength, false);

            calculated.Add(instanceId, new Passphrase() {
                Iv = iv,
                Key = key
            });
        }

        private byte[] GetBytes(int instanceId) {
            var passphrase = Passphrases[instanceId];
            var length = passphrase.Length / 2;
            var pass = new byte[length];

            for (var i = 0; i < length; i++) {
                pass[i] = (byte)Convert.ToInt32(passphrase.Substring(i * 2, 2), Hexadecimal);
            }

            return pass;
        }
    }
}