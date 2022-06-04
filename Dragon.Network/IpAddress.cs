namespace Dragon.Network;

public class IpAddress {
    public string Ip { get; set; }
    public int Port { get; set; }

    public IpAddress() {
        Ip = "0.0.0.0";
    }

    public IpAddress(string ipAddress, int port) {
        Ip = ipAddress;
        Port = port;
    }
}