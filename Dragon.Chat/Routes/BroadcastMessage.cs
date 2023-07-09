using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Chat.Players;
using Dragon.Chat.Network;

using System.Text;

namespace Dragon.Chat.Routes;

public sealed class BroadcastMessage : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.BroadcastMessage;

    public BroadcastMessage(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {     
        var received = packet as PacketBroadcastMessage;

        if (received is not null) {
            var player = FindByConnection(connection);

            if (player is not null) {
                var config = Configuration!.Message;

                var text = received.Text;
                var channel = received.Channel;

                if (text.Length <= config.MaximumLength) {
                    var sender = GetPacketSender();

                    switch (channel) {
                        case ChatChannel.Map:
                            ProcessMapMessage(sender, player, received);
                            break;
                        case ChatChannel.Global:
                            ProcessBroadcastMessage(sender, player, received);
                            break;
                        case ChatChannel.Private:
                            ProcessPrivateMessage(sender, player, received);
                            break;
                        case ChatChannel.Party:
                            ProcessPartyMessage(sender, player, received);
                            break;
                        case ChatChannel.Guild:
                            ProcessLegionMessage(sender, player, received);
                            break;
                    }
                }
            }
        }
    }

    private void ProcessMapMessage(IPacketSender sender, IPlayer player, PacketBroadcastMessage packet) {
        var repository = GetPlayerRepository();

        var players = repository.GetPlayers();

        var bubble = GetBubblePool().GetNextBubble();
        var targets = GetTargetPool().GetNextTarget();

        targets.Reset();

        bubble.Target = player.Name;
        bubble.Color = QbColor.White;
        bubble.TargetType = TargetType.Player;

        var bubbleSize = bubble.Text.Length;
        var messageSize = packet.Text.Length;

        var length = messageSize > bubbleSize ? bubbleSize : messageSize;

        Array.Clear(bubble.Text);

        Buffer.BlockCopy(packet.Text, 0, bubble.Text, 0, length);

        foreach (var (_, target) in players) {
            if (target.InstanceId == player.InstanceId) {
                targets.Players.Add(target);
            }
        }

        packet.Color = QbColor.White;
        packet.AccountLevel = player.AccountLevel;

        sender.SendMessage(packet, targets.Players);
        sender.SendMessageBubble(bubble, targets.Players);
    }

    private void ProcessBroadcastMessage(IPacketSender sender, IPlayer player, PacketBroadcastMessage packet) {
        packet.Color = QbColor.White;
        packet.AccountLevel = player.AccountLevel;

        sender.SendMessage(packet);   
    }

    private void ProcessPrivateMessage(IPacketSender sender, IPlayer player, PacketBroadcastMessage packet) {
        var length = packet.Name.Length;

        if (length > 0) {
            var name = Encoding.ASCII.GetString(packet.Name);

            if (!string.IsNullOrEmpty(name)) {
                var repository = GetPlayerRepository();
                var target = repository.FindByName(name);

                if (target is not null) {
                    if (target.CharacterId != player.CharacterId) {
                        packet.Color = QbColor.BrigthGreen;
                        packet.Channel = ChatChannel.Private;
                        packet.AccountLevel = player.AccountLevel;

                        packet.Name = new byte[length];

                        for (var i = 0; i < length; ++i) {
                            packet.Name[i] = (byte)player.Name[i];
                        }

                        sender.SendMessage(packet, target.Connection);
                    }
                }
                else {
                    sender.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthGreen, player);
                }
            }
        }
    }

    private void ProcessPartyMessage(IPacketSender sender, IPlayer player, PacketBroadcastMessage packet) {
        var repository = GetPlayerRepository();

        var players = repository.GetPlayers();

        var targets = GetTargetPool().GetNextTarget();

        targets.Reset();

        foreach (var (_, target) in players) {
            if (target.PartyId == player.PartyId) {
                targets.Players.Add(target);
            }
        }

        packet.Color = QbColor.White;
        packet.AccountLevel = player.AccountLevel;

        sender.SendMessage(packet, targets.Players);
    }

    private void ProcessLegionMessage(IPacketSender sender, IPlayer player, PacketBroadcastMessage packet) {
        var repository = GetPlayerRepository();

        var players = repository.GetPlayers();

        var targets = GetTargetPool().GetNextTarget();

        targets.Reset();

        foreach (var (_, target) in players) {
            if (target.LegionId == player.LegionId) {
                targets.Players.Add(target);
            }
        }

        packet.Color = QbColor.White;
        packet.AccountLevel = player.AccountLevel;

        sender.SendMessage(packet, targets.Players);
    }
}