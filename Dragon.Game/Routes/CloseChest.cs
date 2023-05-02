using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class CloseChest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.CloseChest;
    public PacketCloseChest? Packet { get; set; }

    private readonly ChestManager ChestManager;

    public CloseChest(IServiceInjector injector) : base(injector) {
        ChestManager = new ChestManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var player = FindByConnection(connection);

        if (player is not null) {
            ChestManager.CloseChest(player);
        }
    }
}