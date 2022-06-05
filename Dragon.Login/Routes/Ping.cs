using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Login.Services;

namespace Dragon.Login.Routes;

public sealed class Ping {
    public IConnection? Connection { get; set; }
    public LoggerService? LoggerService { get; init; }
    public PacketPing? Packet { get; set; }

    public void Process() {

    }
}