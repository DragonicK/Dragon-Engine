using Dragon.Network;

using Dragon.Chat.Messages;
using Dragon.Core.Services;

namespace Dragon.Chat.Network;

public sealed class PacketSender : IPacketSender {

    public PacketSender(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void SendMessage(Message message) {
        //var packet = new PacketBroadcastMessage() {
        //    AccountLevel = message.AccountLevel,
        //    Color = message.Color,
        //    Name = message.Name,
        //    Text = message.Text,
        //    Channel = message.Channel
        //};

        //var msg = Writer!.CreateMessage(packet);

        //msg.TransmissionTarget = TransmissionTarget.Broadcast;

        //Writer.Enqueue(msg);
    }

    public void SendMessage(Message message, IConnection connection) {
        //var packet = new PacketBroadcastMessage() {
        //    AccountLevel = message.AccountLevel,
        //    Color = message.Color,
        //    Name = message.Name,
        //    Text = message.Text,
        //    Channel = message.Channel
        //};

        //var msg = Writer!.CreateMessage(packet);

        //msg.DestinationPeers.Add(player.Connection.Id);
        //msg.TransmissionTarget = TransmissionTarget.Destination;

        //Writer.Enqueue(msg);
    }

    public void SendMessageBubble(Bubble bubble, IConnection connection) {
        //var players = instance.GetPlayers();

        //var packet = new SpMessageBubble() {
        //    TargetType = bubble.TargetType,
        //    Target = bubble.TargetIndex,
        //    Color = bubble.Color,
        //    Text = bubble.Text
        //};

        //var msg = Writer!.CreateMessage(packet);

        //foreach (var player in players) {
        //    msg.DestinationPeers.Add(player.Connection.Id);
        //}

        //msg.TransmissionTarget = TransmissionTarget.Destination;

        //Writer.Enqueue(msg);
    }
}