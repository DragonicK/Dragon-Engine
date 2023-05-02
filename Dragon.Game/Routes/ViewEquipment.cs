using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class ViewEquipment : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.RequestViewEquipment;
    public CpRequestViewEquipment? Packet { get; set; }

    private readonly ViewEquipmentManager ViewEquipmentManager;

    public ViewEquipment(IServiceInjector injector) : base(injector) {
        ViewEquipmentManager = new ViewEquipmentManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpRequestViewEquipment;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                if (IsValidPacket(received)) {
                    var target = GetPlayerRepository().FindByName(received.Character);

                    ViewEquipmentManager.ProcessViewRequest(player, target);
                }
            }
        }      
    }

    private bool IsValidPacket(CpRequestViewEquipment packet) {
        if (packet.Character is null) {
            return false;
        }

        if (packet.Character.Length > Configuration!.Character.MaximumNameLength) {
            return false;
        }

        return true;
    }
}