using Dragon.Core.Model;
using Dragon.Game.Network.Senders;
using Dragon.Game.Players;

namespace Dragon.Game.Manager;

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