namespace Dragon.Core.GeoIpCountry;

public sealed class Country {
    public string IpFrom { get; set; }
    public string IpTo { get; set; }
    public long NumberMin { get; set; }
    public long NumberMax { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public Country() {
        IpFrom = string.Empty;
        IpTo = string.Empty;
        Code = string.Empty;
        Name = string.Empty;
    }
}