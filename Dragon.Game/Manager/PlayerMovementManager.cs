using Dragon.Core.Model;
using Dragon.Core.Services;
using Dragon.Game.Services;
using Dragon.Game.Instances;
using Dragon.Game.Network.Senders;
using Dragon.Game.Players;

namespace Dragon.Game.Manager;

public sealed class PlayerMovementManager {
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private const int Invalid = -1;

    private readonly WarperManager WarperManager;

    public PlayerMovementManager(IServiceInjector injector) {
        injector.Inject(this);

        WarperManager = new WarperManager(injector);
    }

    public void Move(IPlayer player, Direction direction, MovementType movement, int x, int y) {
        var sender = GetPacketSender();

        if (player.Character.X != x || player.Character.Y != y) {
            var instances = InstanceService!.Instances;
            var instanceId = player.Character.Map;

            if (instances.ContainsKey(instanceId)) {
                sender.SendPlayerXY(player, instances[instanceId]);
            }
        }
        else {
            // TODO
            //if (player.ChatNpcId > 0) {
            //    var chat = new NpcConversation(player);
            //    chat.Close();
            //}

            Move(sender, player, direction, movement);
        }
    }

    private void Move(IPacketSender sender, IPlayer player, Direction direction, MovementType movement) {
        player.Character.Direction = direction;

        if (CanMove(player, direction)) {
            var (x, y) = GetNextCoordinate(direction, player.Character.X, player.Character.Y);

            player.Character.X = x;
            player.Character.Y = y;

            var instances = InstanceService!.Instances;
            var instanceId = player.Character.Map;

            instances.TryGetValue(instanceId, out var instance);

            if (instance is not null) {
                if (player.Combat.IsBufferedSkill) {
                    player.Combat.ClearBufferedSkill();

                    sender.SendClearCast(player);
                    sender.SendCancelAnimation(instance, player.IndexOnInstance);
                }

                sender.SendPlayerMovement(player, movement, instance);
            }
        }

        // TODO
        //var tile = player.GetMap().Tile[player.X, player.Y];

        //if (tile.Type == TileType.Warp) {
        //    if (tile.Data1 > 0) {
        //        var map = Global.GetMap(tile.Data1);

        //        if (map != null) {
        //            Warp(map, (short)tile.Data2, (short)tile.Data3);
        //            moved = true;
        //        }
        //    }
        //}
    }

    private bool CanMove(IPlayer player, Direction direction) {
        var instances = InstanceService!.Instances;
        var instanceId = player.Character.Map;

        instances.TryGetValue(instanceId, out var instance);

        if (instance is not null) {
            var x = player.Character.X;
            var y = player.Character.Y;

            if (direction == Direction.Up) {
                if (y > 0) {
                    if (CheckBlockedDirection(player, instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Up > 0) {
                        Warp(player, instance.Link.Up, x, instance.MaximumY);

                        return false;
                    }
                }
            }

            if (direction == Direction.Down) {
                if (y < instance.MaximumY) {
                    if (CheckBlockedDirection(player, instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Down > 0) {
                        Warp(player, instance.Link.Down, x, 0);

                        return false;
                    }
                }
            }

            if (direction == Direction.Left) {
                if (x > 0) {
                    if (CheckBlockedDirection(player, instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Left > 0) {
                        Warp(player, instance.Link.Left, instance.MaximumX, y);

                        return false;
                    }
                }
            }

            if (direction == Direction.Right) {
                if (x < instance.MaximumX) {
                    if (CheckBlockedDirection(player, instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Right > 0) {
                        Warp(player, instance.Link.Right, 0, y);

                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool CheckBlockedDirection(IPlayer player, IInstance instance, Direction direction) {
        var (x, y) = GetNextCoordinate(direction, player.Character.X, player.Character.Y);

        if (instance.IsBlocked(x, y)) {
            return true;
        }

        return false;
    }

    private static (int x, int y) GetNextCoordinate(Direction direction, int x, int y) => direction switch {
        Direction.Up => (x, y -= 1),
        Direction.Down => (x, y += 1),
        Direction.Left => (x -= 1, y),
        Direction.Right => (x += 1, y),
        _ => (Invalid, Invalid)
    };

    private void Warp(IPlayer player, int newInstanceId, int x, int y) {
        var instances = InstanceService!.Instances;

        instances.TryGetValue(newInstanceId, out var instance);

        if (instance is not null) {
            WarperManager.Warp(player, instance, x, y);
        }
    }

    private IPacketSender GetPacketSender() {
        return PacketSenderService!.PacketSender!;
    }
}