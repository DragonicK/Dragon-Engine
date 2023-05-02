using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class UnequipHeraldry : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.UnequipHeraldry;

    private readonly HeraldryManager HeraldryManager;

    public UnequipHeraldry(IServiceInjector injector) : base(injector) {
        HeraldryManager = new HeraldryManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpUnequipHeraldry;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                if (IsValidInventory(received)) {
                    HeraldryManager.UnequipHeraldry(player, received.Index);
                }
            }
        }        
    }

    private bool IsValidInventory(CpUnequipHeraldry packet) {
        var index = packet.Index;

        return index >= 1 && index <= Configuration!.Player.MaximumHeraldries;
    }
}