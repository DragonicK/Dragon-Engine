using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;
using Crystalshire.Game.Manager;

namespace Crystalshire.Game.Routes;

public sealed class DestroyItem {
    public IConnection? Connection { get; set; }
    public CpDestroyItem? Packet { get; set; }
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
                var index = Packet!.InventoryIndex;

                if (IsValidInventory(player, index)) {
                    var manager = new ItemManager() {
                        Player = player,
                        PacketSender = sender,
                        InstanceService = instances,
                        Configuration = Configuration,
                        ContentService = ContentService
                    };

                    manager.DestroyItem(index);
                }
            }
        }
    }

    private static bool IsValidInventory(IPlayer player, int index) {
        return index >= 1 && index <= player.Character.MaximumInventories;
    }
}