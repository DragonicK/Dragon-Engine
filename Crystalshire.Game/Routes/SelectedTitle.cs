using Crystalshire.Network;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Routes {
    public sealed class SelectedTitle {
        public IConnection? Connection { get; set; }
        public PacketSelectedTitle? Packet { get; set; }
        public PacketSenderService? PacketSenderService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }
        public InstanceService? InstanceService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var titles = player.Titles;
                    var index = Packet!.Index;

                    if (index == 0) {
                        player.Character.TitleId = 0;
                        player.Titles.Unequip();
                    }
                    else if (index >= 1 && index <= titles.Count) {
                        index--;

                        var id = titles.GetId(index);

                        if (id > 0) {
                            player.Character.TitleId = id;
                            player.Titles.Equip(id);
                        }
                    }

                    player.AllocateAttributes();
      
                    SendUpdate(player);
                }
            }
         }

        private void SendUpdate(IPlayer player) {
            var sender = PacketSenderService!.PacketSender;

            sender?.SendAttributes(player);

            var instances = PacketSenderService!.InstanceService!.Instances;
            var instanceId = player.Character.Map;

            if (instances.ContainsKey(instanceId)) {
                var instance = instances[instanceId];
  
                sender?.SendPlayerVital(player, instance);
                sender?.SendTitle(player, instance);
            }
        }
    }
}
