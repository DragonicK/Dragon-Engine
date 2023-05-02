using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class DeleteCraft : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.DeleteCraft;

    public DeleteCraft(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        //var player = FindByConnection(connection);

        //if (player is not null) {
        //      TODO
        //}
    }
}