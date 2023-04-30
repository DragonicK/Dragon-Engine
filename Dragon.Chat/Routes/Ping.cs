using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Chat.Network;

namespace Dragon.Chat.Routes;

public sealed class Ping : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.Ping;

    public Ping(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) { 

    }
}