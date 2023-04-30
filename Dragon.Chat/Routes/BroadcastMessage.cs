using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;

using Dragon.Chat.Network;
 
namespace Dragon.Chat.Routes;
public sealed class BroadcastMessage : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.BroadcastMessage;

    public BroadcastMessage(IServiceInjector injector) : base(injector) { }

    public void Process(IConnection connection, object packet) {
        //var received = packet as PacketBroadcastMessage;

        //var repository = ConnectionService!.PlayerRepository;

        //var player = repository!.FindByConnectionId(Connection.Id);

        //if (player is not null) {
        //    var config = Configuration!.Messages;
        //    var text = Packet!.Text;
        //    var channel = Packet!.Channel;

        //    if (text.Length <= config.MaximumLength) {

        //        switch (channel) {
        //            case ChatChannel.Map:
        //                ProcessMapMessage(player);
        //                break;
        //            case ChatChannel.Global:
        //                ProcessBroadcastMessage(player);
        //                break;
        //            case ChatChannel.Private:
        //                ProcessPrivateMessage(player);
        //                break;
        //            case ChatChannel.Party:
        //                ProcessPartyMessage(player);
        //                break;
        //            case ChatChannel.Guild:
        //                ProcessGuildMessage(player);
        //                break;
        //        }
        //    }
        //}
    }

    //private void ProcessMapMessage(IPlayer player) {
    //    var sender = PacketSenderService!.PacketSender;
    //    var instance = GetInstance(player);

    //    if (instance is not null) {
    //        var message = new Message() {
    //            AccountLevel = player.AccountLevel,
    //            Name = player.Character.Name,
    //            Channel = ChatChannel.Map,
    //            Color = QbColor.White,
    //            Text = Packet!.Text
    //        };

    //        var buble = new Bubble() {
    //            Color = QbColor.White,
    //            TargetIndex = player.IndexOnInstance,
    //            TargetType = TargetType.Player,
    //            Text = Packet!.Text
    //        };

    //        sender?.SendMessage(message, instance);
    //        sender?.SendMessageBubble(buble, instance);
    //    }
    //}

    //private void ProcessBroadcastMessage(IPlayer player) {
    //    var sender = PacketSenderService!.PacketSender;
    //    var instance = GetInstance(player);

    //    if (instance is not null) {
    //        var message = new Message() {
    //            AccountLevel = player.AccountLevel,
    //            Name = player.Character.Name,
    //            Channel = ChatChannel.Global,
    //            Color = QbColor.White,
    //            Text = Packet!.Text
    //        };

    //        sender?.SendMessage(message);
    //    }
    //}

    //private void ProcessPrivateMessage(IPlayer player) {
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
    //}

    //// TODO
    //private void ProcessPartyMessage(IPlayer player) {

    //}

    //// TODO
    //private void ProcessGuildMessage(IPlayer player) {

    //}

    //private IInstance? GetInstance(IPlayer player) {
    //    var instances = InstanceService!.Instances;
    //    var instanceId = player.Character.Map;

    //    if (instances.ContainsKey(instanceId)) {
    //        return instances[instanceId];
    //    }

    //    return null;
    //}
}