using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Core.Services;

namespace Dragon.Game.Network;

public interface IPacketRoute {
    MessageHeader Header { get; }
    void Process(IConnection connection, object packet);
}