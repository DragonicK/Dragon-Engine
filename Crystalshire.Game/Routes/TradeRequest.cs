using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;

namespace Crystalshire.Game.Routes {
    public sealed class TradeRequest {
        public IConnection? Connection { get; set; }
        public CpTradeRequest? Packet { get; set; }
        public PacketSenderService? PacketSenderService { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }
        public InstanceService? InstanceService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var trade = new TradeRequestManager() {
                        InstanceService = InstanceService,
                        PacketSender = PacketSenderService!.PacketSender
                    };

                    trade.ProcessRequestInvite(Packet!.Index, player);          
                }
            }
        }
    }
}