using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class RequestBlackMarketItems : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.RequestBlackMarketItems;

    private readonly BlackMarketManager BlackMarketManager;

    public RequestBlackMarketItems(IServiceInjector injector) : base(injector) {
        BlackMarketManager = new BlackMarketManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpRequestBlackMarketItems;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }      
    }

    private void Execute(IPlayer player, CpRequestBlackMarketItems packet) {
        if (IsValidPacket(packet)) {
            BlackMarketManager.SendRequestedItems(player, packet.Category, packet.Page);
        }
    }
         
    private bool IsValidPacket(CpRequestBlackMarketItems packet) {
        return packet.Page > 0;
    }
}