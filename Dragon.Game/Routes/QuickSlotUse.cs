using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class QuickSlotUse : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.QuickSlotUse;

    private const int MaximumQuickSlots = 12;

    private readonly ItemManager ItemManager;

    public QuickSlotUse(IServiceInjector injector) : base(injector) {
        ItemManager = new ItemManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpQuickSlotUse;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Excute(player, received);
            }
        }                 
    }
        
    private void Excute(IPlayer player, CpQuickSlotUse packet) {
        if (IsValidPacket(packet)) {
            var index = packet.Index;
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

    private void UseItem(IPlayer player, int id) {
        var items = ContentService!.Items;

        if (items.Contains(id)) {
            var inventory = player.Inventories.FindByItemId(id);

            if (inventory is not null) {
                ItemManager.UseItem(player, inventory.InventoryIndex);
            }
        }
    }

    private void UseSkill(IPlayer player, int id) {
        // TODO
    }

    private static bool IsValidPacket(CpQuickSlotUse packet) {
        var index = packet.Index;

        return index >= 1 && index <= MaximumQuickSlots;
    }
}