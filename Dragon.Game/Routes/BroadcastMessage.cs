using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Model;

using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Messages;
using Dragon.Game.Instances;

namespace Dragon.Game.Routes;

public sealed class BroadcastMessage {
    public IConnection? Connection { get; set; }
    public PacketBroadcastMessage? Packet { get; set; }
    public PacketSenderService? PacketSenderService { get; init; }
    public ConfigurationService? Configuration { get; init; }
    public ConnectionService? ConnectionService { get; init; }
    public LoggerService? LoggerService { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void Process() {
        var repository = ConnectionService!.PlayerRepository;

        if (Connection is not null) {
            var player = repository!.FindByConnectionId(Connection.Id);

            if (player is not null) {
                var config = Configuration!.Messages;
                var text = Packet!.Text;
                var channel = Packet!.Channel;

                if (text.Length <= config.MaximumLength) {

                    switch (channel) {
                        case ChatChannel.Map:
                            ProcessMapMessage(player);
                            break;
                        case ChatChannel.Global:
                            ProcessBroadcastMessage(player);
                            break;
                        case ChatChannel.Private:
                            ProcessPrivateMessage(player);
                            break;
                        case ChatChannel.Party:
                            ProcessPartyMessage(player);
                            break;
                        case ChatChannel.Guild:
                            ProcessGuildMessage(player);
                            break;
                    }
                }
            }
        }

        Packet.Name = null;
        Packet.Text = null;
        Packet = null;
    }

    private void ProcessMapMessage(IPlayer player) {
        var sender = PacketSenderService!.PacketSender;
        var instance = GetInstance(player);

        if (instance is not null) {
            var message = new Message() {
                AccountLevel = player.AccountLevel,
                Name = player.Character.Name,
                Channel = ChatChannel.Map,
                Color = QbColor.White,
                Text = Packet!.Text
            };

            var buble = new Bubble() {
                Color = QbColor.White,
                TargetIndex = player.IndexOnInstance,
                TargetType = TargetType.Player,
                Text = Packet!.Text
            };

            sender?.SendMessage(message, instance);
            sender?.SendMessageBubble(buble, instance);
        }
    }

    private void ProcessBroadcastMessage(IPlayer player) {
        var sender = PacketSenderService!.PacketSender;
        var instance = GetInstance(player);

        if (instance is not null) {
            var message = new Message() {
                AccountLevel = player.AccountLevel,
                Name = player.Character.Name,
                Channel = ChatChannel.Global,
                Color = QbColor.White,
                Text = Packet!.Text
            };

            sender?.SendMessage(message);
        }
    }

    private void ProcessPrivateMessage(IPlayer player) {
        var sender = PacketSenderService!.PacketSender;
        var name = Packet!.Name.Trim();

        if (!string.IsNullOrEmpty(name)) {
            if (name.Length <= Configuration!.Player.MaximumNameLength) {
                var repository = ConnectionService!.PlayerRepository;
                var dest = repository!.FindByName(name);

                if (dest is not null) {
                    if (dest.Character.CharacterId != player.Character.CharacterId) {
                        var destMessage = new Message() {
                            AccountLevel = player.AccountLevel,
                            Channel = ChatChannel.Private,
                            Name = player.Character.Name,
                            Text = Packet!.Text,
                            Color = QbColor.BrigthGreen
                        };

                        sender!.SendMessage(destMessage, dest);
                    }
                }
                else {
                    sender!.SendMessage(SystemMessage.PlayerIsNotOnline, QbColor.BrigthGreen, player);
                }
            }
        }
    }

    // TODO
    private void ProcessPartyMessage(IPlayer player) {

    }

    // TODO
    private void ProcessGuildMessage(IPlayer player) {

    }

    private IInstance? GetInstance(IPlayer player) {
        var instances = InstanceService!.Instances;
        var instanceId = player.Character.Map;

        if (instances.ContainsKey(instanceId)) {
            return instances[instanceId];
        }

        return null;
    }
}