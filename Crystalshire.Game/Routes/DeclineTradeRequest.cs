using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;

namespace Crystalshire.Game.Routes;

public sealed class DeclineTradeRequest {
    public IConnection? Connection { get; set; }
    public CpDeclineTradeRequest? Packet { get; set; }
    public PacketSenderService? PacketSenderService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public LoggerService? LoggerService { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var trade = new TradeDeclineManager() {
                    InstanceService = InstanceService,
                    PacketSender = PacketSenderService!.PacketSender
                };

                trade.ProcessDeclineRequest(player);
            }
        }
    }
}