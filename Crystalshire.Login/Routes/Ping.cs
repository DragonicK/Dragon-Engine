using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

namespace Crystalshire.Login.Routes;

public sealed class Ping {
    public IConnection? Connection { get; set; }
    public PacketPing? Packet { get; set; }

    public void Process() {

    }
}