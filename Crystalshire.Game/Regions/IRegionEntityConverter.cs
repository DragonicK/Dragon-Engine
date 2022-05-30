using System.Text.Json;
using System.Text.Json.Serialization;

namespace Crystalshire.Game.Regions;

public class IRegionEntityConverter : JsonConverter<IRegionEntity> {
    public override IRegionEntity? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        return JsonSerializer.Deserialize<RegionEntity>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, IRegionEntity value, JsonSerializerOptions options) {
        JsonSerializer.Serialize(writer, value as RegionEntity, typeof(RegionEntity), options);
    }
}