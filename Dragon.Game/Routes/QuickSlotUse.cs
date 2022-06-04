using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model;

using Dragon.Game.Services;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class QuickSlotUse {
    public IConnection? Connection { get; set; }
    public CpQuickSlotUse? Packet { get; set; }
    public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public PacketSenderService? PacketSenderService { get; init; }

    private const int MaximumQuickSlots = 12;

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                if (IsValidPacket()) {
                    var index = Packet!.Index;
                    var quick = player.QuickSlots.Get(index);

                    if (quick is not null) {
                        var type = quick.ObjectType;
                        var id = quick.ObjectValue;

                        switch (type) {
                            case QuickSlotType.Item:
                                UseItem(player, id);
                                break;

                            case QuickSlotType.Skill:
                                UseSkill(player, id);
                                break;
                        }
                    }
                }
            }
        }
    }

    private void UseItem(IPlayer player, int id) {
        var items = ContentService!.Items;

        if (items.Contains(id)) {
            var inventory = player.Inventories.FindByItemId(id);

            if (inventory is not null) {
                var sender = PacketSenderService!.PacketSender;
                var instances = PacketSenderService!.InstanceService;

                var manager = new ItemManager() {
                    Player = player,
                    PacketSender = sender,
                    InstanceService = instances,
                    Configuration = Configuration,
                    ContentService = ContentService
                };

                manager.UseItem(inventory.InventoryIndex);
            }
        }
    }

    private void UseSkill(IPlayer player, int id) {

    }

    private bool IsValidPacket() {
        var index = Packet!.Index;

        return index >= 1 && index <= MaximumQuickSlots;
    }
}