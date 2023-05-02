using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class ViewEquipmentVisibility : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.ViewEquipmentVisibility;

    public ViewEquipmentVisibility(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpViewEquipmentVisibility;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                player.Settings.ViewEquipment = received.IsVisible;
            }
        }      
    }
}