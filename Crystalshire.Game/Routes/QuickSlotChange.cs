using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Core.Model;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes {
    public sealed class QuickSlotChange {
        public IConnection? Connection { get; set; }
        public CpQuickSlotChange? Packet { get; set; }
        public LoggerService? LoggerService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }

        private const int MaximumQuickSlot = 12;

        public void Process() {
            var sender = PacketSenderService!.PacketSender;
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    if (IsValidPacket(player)) {
                        var index = Packet!.QuickSlotIndex;
                        var type = Packet!.QuickSlotType;
                        var from = Packet!.FromIndex;

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

                        sender!.SendQuickSlotUpdate(player, index);
                    }
                }
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

        private bool IsValidPacket(IPlayer player) {
            var type = Packet!.QuickSlotType;
            var index = Packet!.QuickSlotIndex;
            var fromIndex = Packet!.FromIndex;

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
}