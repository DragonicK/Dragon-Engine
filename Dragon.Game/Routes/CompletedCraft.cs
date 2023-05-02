using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class CompletedCraft : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.CompletedCraft;

    private readonly CraftManager CraftManager;

    public CompletedCraft(IServiceInjector injector) : base(injector) {
        CraftManager = new CraftManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var player = FindByConnection(connection);

        if (player is not null) {
            CraftManager.Conclude(player);
        }
    }
}