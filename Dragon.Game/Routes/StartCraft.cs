using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Manager;
using Dragon.Game.Network;
using Dragon.Game.Players;

namespace Dragon.Game.Routes;

public sealed class StartCraft : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.StartCraft;

    private readonly CraftManager CraftManager;

    public StartCraft(IServiceInjector injector) : base(injector) {
        CraftManager = new CraftManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpStartCraft;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                Execute(player, received);
            }
        }
    }

    private void Execute(IPlayer player, CpStartCraft packet) {
        if (IsValidPacket(player, packet)) {
            CraftManager.Start(player, packet.Index);
        }
    }

    private bool IsValidPacket(IPlayer player, CpStartCraft packet) {
        var index = packet.Index;

        if (index <= 0) {
            return false;
        }

        if (index > player.Recipes.Count) {
            return false;
        }

        return true;
    }
}