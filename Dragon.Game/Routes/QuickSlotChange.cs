using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model;

using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class QuickSlotChange : PacketRoute, IPacketRoute {
    public MessageHeader Header { get; set; } = MessageHeader.QuickSlotChange;

    private const int MaximumQuickSlot = 12;

    public QuickSlotChange(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpQuickSlotChange;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }
  
    private void Execute(IPlayer player, CpQuickSlotChange packet) {
        if (IsValidPacket(player, packet)) {
            var index = packet.QuickSlotIndex;
            var type = packet.QuickSlotType;
            var from = packet.FromIndex;

            var sender = GetPacketSender();

            switch (type) {
                case QuickSlotType.None:
                    Clear(player, index);

                    break;

                case QuickSlotType.Item:
                    ChangeFromInventory(player, from, index);

                    break;

                case QuickSlotType.Skill:
                    ChangeFromSkill(player, from, index);

                    break;
            }

            sender.SendQuickSlotUpdate(player, index);
        }
    }

    private void Clear(IPlayer player, int index) {
        player.QuickSlots.Change(index, QuickSlotType.None, 0);
    }

    private void ChangeFromInventory(IPlayer player, int from, int index) {
        var inventory = player.Inventories.FindByIndex(from);

        if (inventory is not null) {
            player.QuickSlots.Change(index, QuickSlotType.Item, inventory.ItemId);
        }
    }

    private void ChangeFromSkill(IPlayer player, int from, int index) {
        from--;

        var inventory = player.Skills.Get(from);

        if (inventory is not null) {
            player.QuickSlots.Change(index, QuickSlotType.Skill, inventory.SkillId);
        }
    }

    private bool IsValidPacket(IPlayer player, CpQuickSlotChange packet) {
        var type = packet.QuickSlotType;
        var index = packet.QuickSlotIndex;
        var fromIndex = packet.FromIndex;

        if (type != QuickSlotType.None) {
            if (index < 1 || index > MaximumQuickSlot) {
                return false;
            }
        }

        if (type == QuickSlotType.Item) {
            if (fromIndex < 1 || fromIndex > player.Character.MaximumInventories) {
                return false;
            }
        }

        if (type == QuickSlotType.Skill) {
            if (fromIndex < 1 || fromIndex > Configuration!.Player.MaximumSkills) {
                return false;
            }
        }

        return true;
    }
}