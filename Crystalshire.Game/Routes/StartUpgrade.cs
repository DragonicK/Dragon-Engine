using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Players;
using Crystalshire.Game.Manager;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes;

public sealed class StartUpgrade {
    public IConnection? Connection { get; set; }
    public CpStartUpgrade? Packet { get; set; }
    public ConfigurationService? Configuration { get; set; }
    public ContentService? ContentService { get; set; }
    public PacketSenderService? PacketSenderService { get; set; }
    public ConnectionService? ConnectionService { get; set; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                if (IsValidInventory(player)) {

                    var manager = new UpgradeManager() {
                        Configuration = Configuration,
                        ContentService = ContentService,
                        PacketSender = sender,
                        Player = player
                    };

                    manager.StartUpgrade(Packet!.InventoryIndex);

                }
            }
        }
    }

    private bool IsValidInventory(IPlayer player) {
        var inventoryIndex = Packet!.InventoryIndex;

        if (inventoryIndex < 1) {
            return false;
        }
        if (inventoryIndex > player.Character.MaximumInventories) {
            return false;
        }

        return true;
    }
}