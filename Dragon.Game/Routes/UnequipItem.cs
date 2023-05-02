using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class UnequipItem : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.UnequipItem;

    private readonly ItemManager ItemManager;

    public UnequipItem(IServiceInjector injector) : base(injector) {
        ItemManager = new ItemManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpUnequipItem;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                ItemManager.UnequipItem(player, received.EquipmentType);
            }
        }
    }
}