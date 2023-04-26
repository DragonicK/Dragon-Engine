using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Chat.Network;
using Dragon.Core.Services;

namespace Dragon.Chat.Routes;

public sealed class Ping : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Ping;

    public void StartInjection(IServiceInjector injector) {

    }

    public void Process(IConnection connection, object packet) { 

    }
}