namespace Dragon.Network;

public class NodeIpAddress : IpAddress {
    public int Id { get; set; }
    public string Name { get; set; }

    public NodeIpAddress() : base() {
        Name = string.Empty;
    }

    public NodeIpAddress(int id, string name, string ipAddress, int port) : base(ipAddress, port) {
        Id = id;
        Name = name;
    }
}