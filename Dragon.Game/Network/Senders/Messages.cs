using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model;

using Dragon.Game.Players;
using Dragon.Game.Messages;
using Dragon.Game.Instances;

namespace Dragon.Game.Network;

public sealed partial class PacketSender {
    public void SendMessage(Message message) {
        var packet = new PacketBroadcastMessage() {
            AccountLevel = message.AccountLevel,
            Color = message.Color,
            Name = message.Name,
            Text = message.Text,
            Channel = message.Channel
        };

        var msg = Writer!.CreateMessage(packet);

        msg.TransmissionTarget = TransmissionTarget.Broadcast;

        Writer.Enqueue(msg);
    }

    public void SendMessage(Message message, IPlayer player) {
        var packet = new PacketBroadcastMessage() {
            AccountLevel = message.AccountLevel,
            Color = message.Color,
            Name = message.Name,
            Text = message.Text,
            Channel = message.Channel
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendMessage(Message message, IInstance instance) {
        var players = instance.GetPlayers();

        var packet = new PacketBroadcastMessage() {
            AccountLevel = message.AccountLevel,
            Color = message.Color,
            Name = message.Name,
            Text = message.Text,
            Channel = message.Channel
        };

        var msg = Writer!.CreateMessage(packet);

        foreach (var player in players) {
            msg.DestinationPeers.Add(player.GetConnection().Id);
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendMessageBubble(Bubble bubble, IInstance instance) {
        var players = instance.GetPlayers();

        var packet = new SpMessageBubble() {
            TargetType = bubble.TargetType,
            Target = bubble.TargetIndex,
            Color = bubble.Color,
            Text = bubble.Text
        };

        var msg = Writer!.CreateMessage(packet);

        foreach (var player in players) {
            msg.DestinationPeers.Add(player.GetConnection().Id);
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendMessage(SystemMessage message, QbColor color, string[]? parameters = null) {
        var packet = new SpSystemMessage() {
            Color = color,
            Message = message,
            Parameters = parameters
        };

        var msg = Writer!.CreateMessage(packet);

        msg.TransmissionTarget = TransmissionTarget.Broadcast;

        Writer.Enqueue(msg);
    }

    public void SendMessage(SystemMessage message, QbColor color, IPlayer player, string[]? parameters = null) {
        var packet = new SpSystemMessage() {
            Color = color,
            Message = message,
            Parameters = parameters
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendMessage(SystemMessage message, QbColor color, IInstance instance, string[]? parameters = null) {
        var players = instance.GetPlayers();

        var packet = new SpSystemMessage() {
            Color = color,
            Message = message,
            Parameters = parameters
        };

        var msg = Writer!.CreateMessage(packet);

        foreach (var player in players) {
            msg.DestinationPeers.Add(player.GetConnection().Id);
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendMessage(ref Damage damage, IInstance instance) {
        var players = instance.GetPlayers();

        var packet = new SpActionMessage() {
            MessageType = damage.MessageType,
            FontType = damage.FontType,
            Message = damage.Message,
            Color = damage.Color,
            Y = damage.Y,
            X = damage.X
        };

        var msg = Writer!.CreateMessage(packet);

        foreach (var player in players) {
            msg.DestinationPeers.Add(player.GetConnection().Id);
        }

        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }
}