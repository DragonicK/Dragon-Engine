using System.Text.Json;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Dragon.Core.Serialization;

public static class Json {
    public static bool FileExists(string json) {
        return File.Exists(json);
    }

    public static T? Get<T>(string file) {
        var options = new JsonSerializerOptions {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        return JsonSerializer.Deserialize<T>(File.ReadAllText(file), options);
    }

    public static T? GetFromText<T>(string text) {
        var options = new JsonSerializerOptions {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        return JsonSerializer.Deserialize<T>(text, options);
    }

    public static void Save<T>(string file, T item) {
        var options = new JsonSerializerOptions {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            WriteIndented = true
        };

        var result = JsonSerializer.Serialize<T>(item, options);

        using var stream = new FileStream(file, FileMode.Create, FileAccess.Write);
        using var writer = new StreamWriter(stream);
        writer.Write(result);
    }
}