using Dragon.Login.Services;

namespace Dragon.Login.Network;

public abstract class Route {
    public GeoIpService? GeoIpService { get; set; }
    public LoggerService? LoggerService { get; set; }
    public DatabaseService? DatabaseService { get; set; }
    public ConnectionService? ConnectionService { get; set; }
    public ConfigurationService? ConfigurationService { get; set; }
    public OutgoingMessageService? OutgoingMessageService { get; set; }
}