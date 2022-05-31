using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crystalshire.Core.Model.Classes;

public class IClassConverter : JsonConverter<IClass> {
    public override IClass? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return JsonSerializer.Deserialize<Class>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, IClass value, JsonSerializerOptions options) {
        JsonSerializer.Serialize(writer, value as Class, typeof(Class), options);
    }
}