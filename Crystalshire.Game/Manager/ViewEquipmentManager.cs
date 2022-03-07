using Crystalshire.Core.Model;

using Crystalshire.Game.Network;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Manager {
    public class ViewEquipmentManager {
        public IPlayer? Player { get; init; }
        public IPacketSender? PacketSender { get; init; }

        public void ProcessViewRequest(IPlayer? target) {
            if (target is not null) {
                if (target.Settings.ViewEquipment) {
                    PacketSender!.SendViewEquipment(Player!, target);
                }
                else {
                    PacketSender!.SendMessage(SystemMessage.ViewEquipmentIsDisabled, QbColor.BrigthRed, Player!);
                }
            }
            else {
                PacketSender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthRed, Player!);
            }
        }
    }
}