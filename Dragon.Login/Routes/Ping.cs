using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

namespace Dragon.Login.Routes;

public sealed class Ping {
    public IConnection? Connection { get; set; }
    public PacketPing? Packet { get; set; }

    public void Process() {

    }
}