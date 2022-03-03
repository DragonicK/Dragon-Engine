namespace Crystalshire.Core.GeoIpCountry {
    public class GeoIpAddressReader : IGeoIpAddressReader {

        private readonly IGeoIpAddress geoIpAddress;

        public GeoIpAddressReader(IGeoIpAddress geoIpAddress) {
            this.geoIpAddress = geoIpAddress;
        }

        public bool Read(string file) {
            try {
                using var fs = File.OpenRead(file);
                using var reader = new StreamReader(fs);

                while (!reader.EndOfStream) {
                    var value = reader.ReadLine();

                    if (value is not null) {
                        var values = value.Split(',');

                        var ipCountry = new Country() {
                            IpFrom = values[0].Replace('"', ' ').Trim(),
                            IpTo = values[1].Replace('"', ' ').Trim(),
                            NumberMin = Convert.ToInt64(values[2].Replace('"', ' ').Trim()),
                            NumberMax = Convert.ToInt64(values[3].Replace('"', ' ').Trim()),
                            Code = values[4].Replace('"', ' ').Trim(),
                            Name = values[5].Replace('"', ' ').Trim()
                        };

                        geoIpAddress.Add(ipCountry);
                    }
                }
            }
            catch {
                return false;
            }

            return true;
        }
    }
}