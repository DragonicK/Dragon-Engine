using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public sealed class PurchaseBlackMarketItem {
        public IConnection? Connection { get; set; }
        public CpPurchaseBlackMarkteItem? Packet { get; set; }
        public ConfigurationService? Configuration { get; set; }
        public ConnectionService? ConnectionService { get; set; }
        public PacketSenderService? PacketSenderService { get; set; }
        public BlackMarketService? BlackMarketService { get; set; }
        public ContentService? ContentService { get; set; }
        public DatabaseService? DatabaseService { get; set; }

        public void Process() {
            if (IsValidPacket()) {
                var sender = PacketSenderService!.PacketSender;
                var repository = ConnectionService!.PlayerRepository;
                var market = BlackMarketService!.BlackMarket;

                if (Connection is not null) {
                    var player = repository!.FindByConnectionId(Connection.Id);

                    if (player is not null) {

                        var manager = new BlackMarketManager() {
                            Player = player,
                            BlackMarket = market,
                            PacketSender = sender,
                            Repository = repository,
                            Configuration = Configuration,
                            ContentService = ContentService,
                            Factory = DatabaseService!.DatabaseFactory
                        };

                        manager.ProcessPurchaseRequest(Packet!.Id, Packet!.Amount, Packet!.Receiver);
                    }
                }
            }
        }

        private bool IsValidPacket() {
            return Packet!.Id > 0;
        }
    }
}