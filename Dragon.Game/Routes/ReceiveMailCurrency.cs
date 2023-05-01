using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class ReceiveMailCurrency : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.ReceiveMailCurrency;

    private readonly ReceiveFromMailManager ReceiveFromMailManager;

    public ReceiveMailCurrency(IServiceInjector injector) : base(injector) {
        ReceiveFromMailManager = new ReceiveFromMailManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpReceiveMailCurrency;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                ReceiveFromMailManager.ReceiveCurrency(player, received.Id);
            }
        }
    }
}