using System.Text.Json.Serialization;

namespace Dragon.Core.Model.Classes;

[JsonConverter(typeof(IClassGrowthConverter))]
public interface IClassGrowth {
    Dictionary<GrowthAttribute, float> Attributes { get; set; }
    float GetAttribute(int level, Dictionary<PrimaryAttribute, int> attributes);
}