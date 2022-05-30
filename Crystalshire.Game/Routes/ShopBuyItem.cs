using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes;

public sealed class ShopBuyItem {
    public IConnection? Connection { get; set; }
    public CpShopBuyItem? Packet { get; set; }
    public ContentService? ContentService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        if (IsValidPacket()) {
            var repository = ConnectionService!.PlayerRepository;
            var sender = PacketSenderService!.PacketSender;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var manager = new ShopManager() {
                        Player = player,
                        PacketSender = sender,
                        Items = ContentService!.Items,
                        Shops = ContentService!.Shops
                    };

                    manager.ProcessBuyRequest(Packet!.Index, Packet!.Amount);
                }
            }
        }
    }

    private bool IsValidPacket() {
        return Packet!.Index >= 1 && Packet.Amount >= 1;
    }
}