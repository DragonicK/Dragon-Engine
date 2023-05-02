using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class ViewEquipmentManager {
    public PacketSenderService? PacketSenderService { get; private set; }

    public ViewEquipmentManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void ProcessViewRequest(IPlayer player, IPlayer? target) {
        var sender = GetPacketSender();

        if (target is not null) {
            if (target.Settings.ViewEquipment) {
                sender.SendViewEquipment(player, target);
            }
            else {
                sender.SendMessage(SystemMessage.ViewEquipmentIsDisabled, QbColor.BrigthRed, player);
            }
        }
        else {
            sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthRed, player);
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}