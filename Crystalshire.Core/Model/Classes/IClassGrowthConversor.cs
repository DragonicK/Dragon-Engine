using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crystalshire.Core.Model.Classes;

public class IClassGrowthConverter : JsonConverter<IClassGrowth> {
    public override IClassGrowth? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return JsonSerializer.Deserialize<ClassGrowth>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, IClassGrowth value, JsonSerializerOptions options) {
        JsonSerializer.Serialize(writer, value as ClassGrowth, typeof(ClassGrowth), options);
    }
}