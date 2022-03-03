using System.Text.Json.Serialization;

namespace Crystalshire.Core.GeoIpCountry {
    public class BlockedCountry {
        public List<string> Countries { get; set; }

        [JsonIgnore]
        private const int InitialCapacity = 320;

        public BlockedCountry() {
            Countries = new List<string>(InitialCapacity);
        }

        public void Add(string code) {
            Countries.Add(code);
        }

        public void Clear() {
            Countries.Clear();
        }

        public bool IsBlocked(string code) {
            return (Countries.FirstOrDefault(x => x.CompareTo(code) == 0) is not null);
        }
    }
}