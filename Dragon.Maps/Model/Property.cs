using System.Text;

using Dragon.Core.Cryptography;

namespace Dragon.Maps.Model;

public class Property : IProperty {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Music { get; set; }
    public string Ambience { get; set; }
    public Link Link { get; set; }
    public Moral Moral { get; set; }
    public Weather Weather { get; set; }
    public Boot Boot { get; set; }
    public Fog Fog { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public string KeyA { get; set; }
    public string KeyB { get; set; }
    public string KeyC { get; set; }

    private const int KeySize = 16;

    public Property() {
        Name = "無名";
        Music = string.Empty;
        Ambience = string.Empty;
        KeyA = string.Empty;
        KeyB = string.Empty;
        KeyC = string.Empty;

        Width = 1;
        Height = 1;
    }

    public string GetHashText() {
        var hash = GetHash();
        var r = new StringBuilder(hash.Length * 2);

        for (var i = 0; i < hash.Length; ++i) {
            r.Append(hash[i].ToString("x2"));
        }

        return r.ToString();
    }

    public byte[] GetHash() {
        var hash = Hash.Compute(KeyA + KeyB + KeyC);

        if (hash.Length >= KeySize) {
            var nHash = new byte[KeySize];

            Array.Copy(hash, nHash, KeySize);

            return nHash;
        }

        return hash;
    }
}