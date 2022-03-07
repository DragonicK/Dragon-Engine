using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Routes {
    public sealed class ViewEquipment {
        public IConnection? Connection { get; set; }
        public CpRequestViewEquipment? Packet { get; set; }
        public ConnectionService? ConnectionService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }  

        public void Process() {
            if (IsValidPacket()) {
                var repository = ConnectionService!.PlayerRepository;
                var sender = PacketSenderService!.PacketSender;

                if (Connection is not null) {
                    var player = repository!.FindByConnectionId(Connection.Id);

                    if (player is not null) {
                        var target = repository!.FindByName(Packet!.Character);

                        var manager = new ViewEquipmentManager() {
                            Player = player,
                            PacketSender = sender
                        };

                        manager.ProcessViewRequest(target);
                    }
                }
            }
        }

        private bool IsValidPacket() {
            if (Packet!.Character is null) {
                return false;
            }

            if (Configuration is not null) {
                if (Packet!.Character.Length > Configuration.Character.MaximumNameLength) {
                    return false;
                }
            }

            return true;
        }
    }
}