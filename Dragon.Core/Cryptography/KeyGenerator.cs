using System.Text;
using System.Security.Cryptography;

namespace Dragon.Core.Cryptography;

public static class KeyGenerator {

    private const string Words = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

    /// <summary>
    /// Quantidade de caracteres da chave.
    /// </summary>
    private const int Length = 15;

    public static string GetUniqueKey() {
        var chars = Words.ToCharArray();
        var data = new byte[Length];

        using (var crypto = RandomNumberGenerator.Create()) {
            crypto.GetBytes(data);
        }

        var result = new StringBuilder(Length);

        foreach (byte b in data) {
            result.Append(chars[b % (chars.Length)]);
        }

        return result.ToString();
    }
}