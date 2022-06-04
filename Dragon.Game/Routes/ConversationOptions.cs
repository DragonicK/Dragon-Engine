using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Services;
using Dragon.Game.Manager;

namespace Dragon.Game.Routes;

public sealed class ConversationOptions {
    public IConnection? Connection { get; set; }
    public PacketConversationOption? Packet { get; set; }
    public PacketSenderService? PacketSenderService { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public LoggerService? LoggerService { get; init; }
    public ContentService? ContentService { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void Process() {
        if (IsValidPacket()) {
            var repository = ConnectionService!.PlayerRepository;
            var sender = PacketSenderService!.PacketSender;

            if (Connection is not null) {
                var player = repository!.FindByConnectionId(Connection.Id);

                if (player is not null) {
                    var manager = new ConversationManager() {
                        Player = player,
                        PacketSender = sender,
                        Shops = ContentService!.Shops,
                        Effects = ContentService!.Effects,
                        InstanceService = InstanceService,
                        Conversations = ContentService!.Conversations
                    };

                    manager.ProcessOptions(Packet!.ConversationId, Packet.ChatIndex, Packet.Option);
                }
            }
        }
    }

    private bool IsValidPacket() {
        if (Packet!.ConversationId <= 0) {
            return false;
        }

        if (Packet!.ChatIndex <= 0) {
            return false;
        }

        return true;
    }
}