using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Manager;
using Dragon.Game.Services;

namespace Dragon.Game.Routes;

public sealed class SelectedItemToUpgrade {
    public IConnection? Connection { get; set; }
    public CpSelectedItemToUpgrade? Packet { get; set; }
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

                    var manager = new ItemUpgradeManager() {
                        Configuration = Configuration,
                        ContentService = ContentService,
                        PacketSender = sender,
                        Player = player
                    };

                    manager.SendUpgradeData(Packet!.InventoryIndex);

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