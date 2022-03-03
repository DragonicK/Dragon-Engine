using Crystalshire.Core.Model;
using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Players;
using Crystalshire.Game.Messages;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Network {
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
            var list = players.Select(p => p.GetConnection().Id);

            var packet = new PacketBroadcastMessage() {
                AccountLevel = message.AccountLevel,
                Color = message.Color,
                Name = message.Name,
                Text = message.Text,
                Channel = message.Channel
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.AddRange(list);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendMessageBubble(Bubble bubble, IInstance instance) {
            var players = instance.GetPlayers();
            var list = players.Select(p => p.GetConnection().Id);

            var packet = new SpMessageBubble() {
                TargetType = bubble.TargetType,
                Target = bubble.TargetIndex,
                Color = bubble.Color,
                Text = bubble.Text
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.AddRange(list);
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
            var list = players.Select(p => p.GetConnection().Id);

            var packet = new SpSystemMessage() {
                Color = color,
                Message = message,
                Parameters = parameters
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.AddRange(list);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendMessage(Damage damage, IPlayer player) {
            var packet = new SpActionMessage() {
                MessageType = damage.MessageType,
                FontType = damage.FontType,
                Message = damage.Message,
                Color = damage.Color,
                Y = damage.Y,
                X = damage.X
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendMessage(Damage damage, IInstance instance) {
            var players = instance.GetPlayers();
            var list = players.Select(p => p.GetConnection().Id);

            var packet = new SpActionMessage() {
                MessageType = damage.MessageType,
                FontType = damage.FontType,
                Message = damage.Message,
                Color = damage.Color,
                Y = damage.Y,
                X = damage.X
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.AddRange(list);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }
}