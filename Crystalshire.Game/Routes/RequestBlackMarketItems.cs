using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Manager;

namespace Crystalshire.Game.Routes {
    public sealed class RequestBlackMarketItems {
        public IConnection? Connection { get; set; }
        public CpRequestBlackMarketItems? Packet { get; set; }
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

                        manager.SendRequestedItems(Packet!.Category, Packet.Page);
                    }
                }
            }
        }

        private bool IsValidPacket() {
            return Packet!.Page > 0;
        }
    }
}