using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;
using Crystalshire.Game.Manager;

namespace Crystalshire.Game.Routes;

public sealed class EquipHeraldry {
    public IConnection? Connection { get; set; }
    public CpEquipHeraldry? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    public void Process() {
        var sender = PacketSenderService!.PacketSender;
        var instances = PacketSenderService!.InstanceService;
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                if (IsValidInventory(player)) {

                    var manager = new HeraldryManager() {
                        Player = player,
                        PacketSender = sender,
                        InstanceService = instances,
                        Configuration = Configuration,
                        Heraldries = ContentService!.Heraldries,
                        Items = ContentService!.Items
                    };

                    manager.EquipHeraldryAtIndewx(Packet!.HeraldryIndex, Packet!.InventoryIndex);
                }
            }
        }
    }

    private bool IsValidInventory(IPlayer player) {
        var inventoryIndex = Packet!.InventoryIndex;
        var heraldryIndex = Packet!.HeraldryIndex;

        if (heraldryIndex < 1 || inventoryIndex < 1) {
            return false;
        }

        if (heraldryIndex > Configuration!.Player.MaximumHeraldries) {
            return false;
        }

        if (inventoryIndex > player.Character.MaximumInventories) {
            return false;
        }

        return true;
    }
}