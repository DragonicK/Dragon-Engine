using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class TakeItemFromChest : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.TakeItemFromChest;

    private readonly ChestManager ChestManager;

    public TakeItemFromChest(IServiceInjector injector) : base(injector) {
        ChestManager = new ChestManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpTakeItemFromChest;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpTakeItemFromChest packet) {
        if (IsValidPacket(packet)) {
            ChestManager.TakeItem(player, packet.Index);
        }   
    }

    private bool IsValidPacket(CpTakeItemFromChest packet) {
        return packet.Index > 0;
    }
}