using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Outgoing;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Chat.Players;
using Dragon.Chat.Services;

namespace Dragon.Chat.Network;

public sealed class PacketSender : IPacketSender {
    public OutgoingMessageService? OutgoingMessageService { get; private set; }

    public PacketSender(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void SendMessage(PacketBroadcastMessage message) {
        var writer = GetOutgoingMessageWriter();

        var msg = writer.CreateMessage(message);

        msg.TransmissionTarget = TransmissionTarget.Broadcast;

        writer.Enqueue(msg);
    }

    public void SendMessage(PacketBroadcastMessage message, IConnection connection) {
        var writer = GetOutgoingMessageWriter();

        var msg = writer.CreateMessage(message);

        msg.DestinationPeers.Add(connection.Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);
    }

    public void SendMessage(PacketBroadcastMessage message, IList<IPlayer> players) {
        var writer = GetOutgoingMessageWriter();

        var msg = writer.CreateMessage(message);

        foreach (var player in players) {
            msg.DestinationPeers.Add(player.Connection.Id);
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);
    }

    public void SendMessageBubble(SpMessageBubble bubble, IList<IPlayer> players) {
        var writer = GetOutgoingMessageWriter();

        var msg = writer.CreateMessage(bubble);

        foreach (var player in players) {
            msg.DestinationPeers.Add(player.Connection.Id);
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        writer.Enqueue(msg);
    }

    private IOutgoingMessageWriter GetOutgoingMessageWriter() {
        return OutgoingMessageService!.OutgoingMessageWriter!;
    }
}