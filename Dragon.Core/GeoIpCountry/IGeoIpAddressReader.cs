namespace Dragon.Core.GeoIpCountry;

public interface IGeoIpAddressReader {
    bool Read(string file);
}