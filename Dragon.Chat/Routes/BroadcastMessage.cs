using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Chat.Players;
using Dragon.Chat.Network;

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
                    switch (channel) {
                        case ChatChannel.Map:
                            ProcessMapMessage(player, received);
                            break;
                        case ChatChannel.Global:
                            ProcessBroadcastMessage(player, received);
                            break;
                        case ChatChannel.Private:
                            ProcessPrivateMessage(player, received);
                            break;
                        case ChatChannel.Party:
                            ProcessPartyMessage(player, received);
                            break;
                        case ChatChannel.Guild:
                            ProcessLegionMessage(player, received);
                            break;
                    }
                }
            }
        }
    }

    private void ProcessMapMessage(IPlayer player, PacketBroadcastMessage packet) {
        var sender = GetPacketSender();
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

        packet.AccountLevel = player.AccountLevel;
        packet.Color = QbColor.White;

        sender.SendMessage(packet, targets.Players);
        sender.SendMessageBubble(bubble, targets.Players);
    }

    private void ProcessBroadcastMessage(IPlayer player, PacketBroadcastMessage packet) {
        packet.AccountLevel = player.AccountLevel;
        packet.Color = QbColor.White;

        GetPacketSender().SendMessage(packet);   
    }

    private void ProcessPrivateMessage(IPlayer player, PacketBroadcastMessage packet) {
    //    var sender = PacketSenderService!.PacketSender;
    //    var name = Packet!.Name.Trim();

    //    if (!string.IsNullOrEmpty(name)) {
    //        if (name.Length <= Configuration!.Player.MaximumNameLength) {
    //            var repository = ConnectionService!.PlayerRepository;
    //            var dest = repository!.FindByName(name);

    //            if (dest is not null) {
    //                if (dest.Character.CharacterId != player.Character.CharacterId) {
    //                    var destMessage = new Message() {
    //                        AccountLevel = player.AccountLevel,
    //                        Channel = ChatChannel.Private,
    //                        Name = player.Character.Name,
    //                        Text = Packet!.Text,
    //                        Color = QbColor.BrigthGreen
    //                    };

    //                    sender!.SendMessage(destMessage, dest);
    //                }
    //            }
    //            else {
    //                sender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthGreen, player);
    //            }
    //        }
    //    }
    }

    private void ProcessPartyMessage(IPlayer player, PacketBroadcastMessage packet) {

    }

    private void ProcessLegionMessage(IPlayer player, PacketBroadcastMessage packet) {

    }
}