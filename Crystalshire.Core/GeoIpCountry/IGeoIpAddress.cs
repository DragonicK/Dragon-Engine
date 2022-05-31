namespace Crystalshire.Core.GeoIpCountry;

public interface IGeoIpAddress {
    void Add(Country geoIpAddress);
    void Clear();
    bool IsCountryBlocked(string ipAddress);
    Country? GetBlockedCountry(string ipAddress);
}