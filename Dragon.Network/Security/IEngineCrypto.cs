namespace Dragon.Network.Security;

public interface IEngineCrypto {
    bool IsKeyAlreadyUdpated { get; set; }
    void UpdateKey(byte[] key);
    void Cipher(byte[] buffer, int offset, int length);
    bool Decipher(byte[] buffer, int offset, int length);
    void AppendCheckSum(byte[] buffer, int offset, int length);
}