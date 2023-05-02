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
        var player = FindByConnection(connection);

        if (player is not null) {
            if (player.AccountLevel >= AccountLevel.Monitor) {
                Execute(player, packet as CpSuperiorCommand);
            }
        }
    }

    private void Execute(IPlayer player, CpSuperiorCommand? packet) {
        if (packet is not null) {
            var instance = GetCommandRepository().GetType(packet.Command);

            instance?.Process(player, packet.Parameters);
        }
    }

    private ICommandRepository GetCommandRepository() {
        return ContentService!.CommandRepository;
    }
}