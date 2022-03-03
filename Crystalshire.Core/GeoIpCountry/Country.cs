namespace Crystalshire.Core.GeoIpCountry {
    public class Country {
        public string IpFrom { get; set; }
        public string IpTo { get; set; }
        public long NumberMin { get; set; }
        public long NumberMax { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public Country() {
            IpFrom = String.Empty;
            IpTo = String.Empty;
            Code = String.Empty;
            Name = String.Empty;
        }
    }
}