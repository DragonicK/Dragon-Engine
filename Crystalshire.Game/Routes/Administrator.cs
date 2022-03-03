using Crystalshire.Core.Model;
using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Services;
using Crystalshire.Game.Players;
using Crystalshire.Game.Administrator;

namespace Crystalshire.Game.Routes {
    public sealed class Administrator {
        public IConnection? Connection { get; set; }
        public CpSuperiorCommand? Packet { get; set; }
        public PacketSenderService? PacketSenderService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public InstanceService? InstanceService { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public LoggerService? LoggerService { get; init; }
        public ContentService? ContentService { get; init; }

        public void Process() {
            var repository = ConnectionService!.PlayerRepository;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    if (player.AccountLevel >= AccountLevel.Monitor) {
                        CreateInstance(player);
                    }
                }
            }
        }

        private void CreateInstance(IPlayer player) {
            var header = Packet!.Command;
            var commands = ContentService!.CommandRepository;

            var type = commands.GetType(header);

            if (type is not null) {
                var instance = Activator.CreateInstance(type) as IAdministratorCommand;

                if (instance is not null) {
                    instance.PacketSender = PacketSenderService!.PacketSender;
                    instance.ConnectionService = ConnectionService;
                    instance.InstanceService = InstanceService;
                    instance.ContentService = ContentService;
                    instance.Configuration = Configuration;
                    instance.Administrator = player;

                    instance.Process(Packet!.Parameters);
                }
            }
        }
    }
}