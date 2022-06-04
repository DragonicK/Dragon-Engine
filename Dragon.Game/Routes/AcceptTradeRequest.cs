using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class AcceptTradeRequest {
    public IConnection? Connection { get; set; }
    public CpAcceptTradeRequest? Packet { get; set; }
    public PacketSenderService? PacketSenderService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public LoggerService? LoggerService { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var trade = new TradeAcceptManager() {
                    InstanceService = InstanceService,
                    PacketSender = PacketSenderService!.PacketSender
                };

                trade.ProcessAcceptRequest(player);
            }
        }
    }
}