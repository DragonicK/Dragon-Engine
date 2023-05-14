using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Manager;

public sealed class WarperManager {
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    public WarperManager(IServiceInjector injector) {
        injector.Inject(this);
    }

    public void Warp(IPlayer player, IInstance instance, int x, int y) {
        if (x > instance.MaximumX) {
            x = instance.MaximumX;
        }

        if (y > instance.MaximumY) {
            y = instance.MaximumY;
        }

        if (x < 0) {
            x = 0;
        }

        if (y < 0) {
            y = 0;
        }

        var sender = GetPacketSender();

        var contains = false;

        // Check if player is trying to teleport to same instance.
        if (player.Character.Map == instance.Id) {
            contains = instance.Contains(player);

            if (contains) {
                player.Character.X = x;
                player.Character.Y = y;

                sender.SendPlayerXY(player, instance);
            }
        }

        if (!contains) {
            sender.SendGettingMap(player, true);

            RemoveFromLastInstance(sender, player, instance);

            AddPlayerToInstance(sender, player, instance, x, y);

            sender.SendGettingMap(player, false);
        }
    }

    private void RemoveFromLastInstance(IPacketSender sender, IPlayer player, IInstance instance) {
        var instanceId = player.Character.Map;

        if (instanceId != instance.Id) {
            var index = player.IndexOnInstance;

            var _instance = InstanceService!.Instances[instanceId];

            var removed = _instance.Remove(player);

            if (removed) {
                sender.SendPlayerLeft(player, _instance, index);
                sender.SendHighIndex(_instance);
            }
        }
    }

    private void AddPlayerToInstance(IPacketSender sender, IPlayer player, IInstance instance, int x, int y) {
        var added = instance.Add(player);

        if (added) {
            player.Target = null;
            player.TargetType = TargetType.None;

            sender.SendTarget(player, TargetType.None, 0);

            // TODO
            // map.SendCorpseTo(player);

            player.Character.Map = instance.Id;
            player.Character.X = x;
            player.Character.Y = y;

            sender.SendLoadMap(player);
            sender.SendClearPlayers(player);
            sender.SendPlayerIndex(player);
            sender.SendHighIndex(instance);

            sender.SendInstanceEntities(player, instance);

            sender.SendPlayerDataTo(player, instance);
            sender.SendPlayersOnMapTo(player, instance);

            sender.SendChests(player, instance);

            foreach (var member in instance.GetPlayers()) {
                sender.SendPlayerVital(member, instance);
                sender.SendDisplayIcons(member, instance);
            }
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}