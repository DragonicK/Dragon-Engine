 namespace Dragon.Network;

public interface IEngineCrypto {
    void UpdateKey(byte[] key); 
    void Cipher(byte[] buffer, int offset, int length);
    bool Decipher(byte[] buffer, int offset, int length);
    void AppendCheckSum(byte[] buffer, int offset, int length); 
}