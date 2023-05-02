using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Players;
using Dragon.Game.Network;

namespace Dragon.Game.Routes;

public sealed class SelectedTitle : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.SelectedTitle;

    public SelectedTitle(IServiceInjector injector) : base(injector) { }

    public PacketSelectedTitle? Packet { get; set; }

    public void Process(IConnection connection, object packet) {
        var received = packet as PacketSelectedTitle;

        if (received is not null) {
            var player = GetPlayerRepository().FindByConnectionId(connection.Id);

            if (player is not null) {
                Execute(player, received);
            }
        }       
    }

    private void Execute(IPlayer player, PacketSelectedTitle packet) {
        var titles = player.Titles;
        var index = packet.Index;

        if (index == 0) {
            player.Character.TitleId = 0;
            player.Titles.Unequip();
        }
        else if (index >= 1 && index <= titles.Count) {
            index--;

            var id = titles.GetId(index);

            if (id > 0) {
                player.Character.TitleId = id;
                player.Titles.Equip(id);
            }
        }

        player.AllocateAttributes();

        SendUpdate(player);
    }

    private void SendUpdate(IPlayer player) {
        var sender = GetPacketSender();

        sender.SendAttributes(player);

        var instances = GetInstances();
        var instanceId = player.Character.Map;

        instances.TryGetValue(instanceId, out var instance);

        if (instance is not null) {
            sender.SendPlayerVital(player, instance);
            sender.SendTitle(player, instance);
        }
    }
}