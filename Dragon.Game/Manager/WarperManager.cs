using Dragon.Core.Model;

using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Instances;

namespace Dragon.Game.Manager;

public class WarperManager {
    public IPlayer? Player { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void Warp(IInstance instance, int x, int y) {
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

        var contains = false;

        // Check if player is trying to teleport to same instance.
        if (Player!.Character.Map == instance.Id) {
            contains = instance.Contains(Player);

            if (contains) {
                Player.Character.X = x;
                Player.Character.Y = y;

                PacketSender?.SendPlayerXY(Player, instance);
            }
        }

        if (!contains) {
            PacketSender?.SendGettingMap(Player, true);

            RemoveFromLastInstance(instance);

            AddPlayerToInstance(instance, x, y);

            PacketSender?.SendGettingMap(Player, false);
        }
    }

    private void RemoveFromLastInstance(IInstance instance) {
        // Remove character from last instance.
        var instanceId = Player!.Character.Map;

        if (instanceId != instance.Id) {
            var index = Player.IndexOnInstance;

            var _instance = InstanceService!.Instances[instanceId];

            var removed = _instance.Remove(Player);

            if (removed) {
                PacketSender?.SendPlayerLeft(Player, _instance, index);
                PacketSender?.SendHighIndex(_instance);
            }
        }
    }

    private void AddPlayerToInstance(IInstance instance, int x, int y) {
        var added = instance.Add(Player!);

        if (added) {
            Player!.Target = null;
            Player.TargetType = TargetType.None;

            PacketSender!.SendTarget(Player, TargetType.None, 0);

            // TODO
            // map.SendCorpseTo(player);

            //var action = new RequirementEntry() {
            //    SecondaryType = (int)InstanceType.Enter,
            //    Id = map.Id
            //};

            //AchievementEngine.CheckAchievement(player, AchievementPrimaryRequirement.Instance, action);

            Player!.Character.Map = instance.Id;
            Player.Character.X = x;
            Player.Character.Y = y;

            PacketSender!.SendLoadMap(Player);
            PacketSender!.SendClearPlayers(Player);
            PacketSender!.SendPlayerIndex(Player);
            PacketSender!.SendHighIndex(instance);

            PacketSender!.SendInstanceEntities(Player, instance);

            PacketSender!.SendPlayerDataTo(Player, instance);
            PacketSender!.SendPlayersOnMapTo(Player, instance);

            PacketSender!.SendChests(Player, instance);

            foreach (var player in instance.GetPlayers()) {
                PacketSender!.SendPlayerVital(player, instance);
                PacketSender!.SendDisplayIcons(player, instance);
            }
        }
    }
}