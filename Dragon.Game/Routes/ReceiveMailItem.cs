using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class ReceiveMailItem : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.ReceiveMailItem;

    private readonly ReceiveFromMailManager ReceiveFromMailManager;

    public ReceiveMailItem(IServiceInjector injector) : base(injector) {
        ReceiveFromMailManager = new ReceiveFromMailManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpReceiveMailItem;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                ReceiveFromMailManager.ReceiveItem(player, received.Id);
            }
        }
    }
}