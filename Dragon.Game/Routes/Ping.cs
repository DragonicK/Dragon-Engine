using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class Ping : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Ping;

    public void Process(IConnection connection, object packet) {
 
    }

    public void StartInjection(IServiceInjector injector) {
  
    }
}