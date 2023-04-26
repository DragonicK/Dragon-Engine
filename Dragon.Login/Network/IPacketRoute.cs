using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

namespace Dragon.Login.Network;

public interface IPacketRoute {
    MessageHeader Header { get; }
    void StartInjection(IServiceInjector injector);
    void Process(IConnection connection, object packet);
}