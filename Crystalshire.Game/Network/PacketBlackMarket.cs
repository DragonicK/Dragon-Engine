using Crystalshire.Core.Network;
using Crystalshire.Core.Model.BlackMarket;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Players;

namespace Crystalshire.Game.Network {
    public sealed partial class PacketSender {
        public void SendBlackMarketItems(IPlayer player, BlackMarketItem[]? items, int maximumCategoryPages) {
            var packet = new SpBlackMarketItems() {
                MaximumCategoryPages = maximumCategoryPages
            };

            if (items is not null) {
                packet.Items = items;
            }

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendCash(IPlayer player) {
            if (player.Account is not null) {
                var packet = new SpCash() {
                    Cash = player.Account.Cash
                };

                var msg = Writer!.CreateMessage(packet);

                msg.DestinationPeers.Add(player.GetConnection().Id);
                msg.TransmissionTarget = TransmissionTarget.Destination;

                Writer.Enqueue(msg);
            }
        }
    }
}