using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class PurchaseBlackMarketItem : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.PurchaseBlackMarketItem;

    private readonly BlackMarketManager BlackMarketManager;

    public PurchaseBlackMarketItem(IServiceInjector injector) : base(injector) {
        BlackMarketManager = new BlackMarketManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpPurchaseBlackMarkteItem;

        if (received is not null) {
            if (IsValidPacket(received)) {
                var player = GetPlayerRepository().FindByConnectionId(connection.Id);

                if (player is not null) {
                    BlackMarketManager.ProcessPurchaseRequest(player, received.Id, received.Amount, received.Receiver);
                }
            }
        }
    }

    private bool IsValidPacket(CpPurchaseBlackMarkteItem packet) {
        return packet.Id > 0;
    }
}