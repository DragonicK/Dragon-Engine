using Dragon.Core.Services;
using Dragon.Core.Model.Crafts;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class StopCraft : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.StopCraft;

    public StopCraft(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpStartUpgrade;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                player.Craft.ProcessingRecipeId = 0;
                player.Craft.State = CraftState.Stopped;
            }
        }
    }
}