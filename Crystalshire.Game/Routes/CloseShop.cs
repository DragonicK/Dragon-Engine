using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public sealed class CloseShop {
        public IConnection? Connection { get; set; }
        public CpShopClose? Packet { get; set; }
        public ConnectionService? ConnectionService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    player.ShopId = 0;
                }
            }
        }
    }
}