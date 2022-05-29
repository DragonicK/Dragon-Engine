namespace Crystalshire.Core.GeoIpCountry {
    public class GeoIpAddress : IGeoIpAddress {
        private const int InitialCapacity = 200000;

        private readonly HashSet<Country> addresses;
        private readonly BlockedCountry blockedCountry;

        public GeoIpAddress(BlockedCountry blockedCountry) {
            this.blockedCountry = blockedCountry;
            addresses = new HashSet<Country>(InitialCapacity);
        }

        public void Add(Country geoIpAddress) {
            addresses.Add(geoIpAddress);
        }

        public void Clear() {
            addresses.Clear();
        }

        public bool IsCountryBlocked(string ipAddress) {
            var country = GetBlockedCountry(ipAddress);

            if (country is null) {
                return false;
            }

            return blockedCountry.IsBlocked(country.Code);
        }

        public Country? GetBlockedCountry(string ipAddress) {
            var addressNumber = GetIpNumber(ipAddress);

            var country = from address in addresses
                          where address.NumberMin <= addressNumber && address.NumberMax >= addressNumber
                          select address;

            return country.FirstOrDefault();
        }

        /// <summary>
        /// Faz a soma dos valores e retorna o número do endereço.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private long GetIpNumber(string ipAddress) {
            if (ipAddress == "::1") {
                ipAddress = "127.0.0.1";
            }

            var ips = ipAddress.Split('.');

            var w = long.Parse(ips[0]) * 16777216;
            var x = long.Parse(ips[1]) * 65536;
            var y = long.Parse(ips[2]) * 256;
            var z = long.Parse(ips[3]);

            return w + x + y + z;
        }
    }
}