using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Network;
using Dragon.Game.Administrator;

namespace Dragon.Game.Routes;

public sealed class Administrator : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.SuperiorCommand;

    public Administrator(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var player = GetPlayerRepository().FindByConnectionId(connection.Id);

        if (player is not null) {
            if (player.AccountLevel >= AccountLevel.Monitor) {
                Execute(player, packet as CpSuperiorCommand);
            }
        }
    }

    private void Execute(IPlayer player, CpSuperiorCommand? packet) {
        if (packet is not null) {
            var header = packet.Command;
            var commands = ContentService!.CommandRepository;

            var type = commands.GetType(header);

            if (type is not null) {
                var instance = Activator.CreateInstance(type, ServiceContainer) as IAdministratorCommand;

                if (instance is not null) {
                    instance.Administrator = player;
                    instance.ContentService = ContentService;
                    instance.InstanceService = InstanceService;
                    instance.Configuration = Configuration;
                    instance.ConnectionService = ConnectionService;
                    instance.PacketSender = PacketSenderService!.PacketSender;

                    instance.Process(packet.Parameters);
                }
            }
        }
    }
}