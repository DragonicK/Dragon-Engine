using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class ConversationOptions : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.ConversationOption;

    private readonly ConversationManager ConversationManager;

    public ConversationOptions(IServiceInjector injector) : base(injector) {
        ConversationManager = new ConversationManager(injector);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as PacketConversationOption;

        if (received is not null) {
            if (IsValidPacket(received)) {
                var player = GetPlayerRepository().FindByConnectionId(connection.Id);

                if (player is not null) {
                    ConversationManager.ProcessOptions(player, received.ConversationId, received.ChatIndex, received.Option);
                }
            }
        }
    }

    private bool IsValidPacket(PacketConversationOption packet) {
        if (packet.ConversationId <= 0) {
            return false;
        }

        if (packet.ChatIndex <= 0) {
            return false;
        }

        return true;
    }
}