using Crystalshire.Core.Model;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Players;

public class PlayerMovement {
    public IPlayer? Player { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }

    private const int Invalid = -1;

    public void Move(Direction direction, MovementType movement, int x, int y) {
        if (Player!.Character.X != x || Player!.Character.Y != y) {
            var instances = InstanceService!.Instances;
            var instanceId = Player!.Character.Map;

            if (instances.ContainsKey(instanceId)) {
                PacketSender!.SendPlayerXY(Player, instances[instanceId]);
            }
        }
        else {
            // TODO
            //if (player.ChatNpcId > 0) {
            //    var chat = new NpcConversation(player);
            //    chat.Close();
            //}

            Move(direction, movement);
        }
    }

    private void Move(Direction direction, MovementType movement) {
        Player!.Character.Direction = direction;

        if (CanMove(direction)) {
            var (x, y) = GetNextCoordinate(direction, Player.Character.X, Player.Character.Y);

            Player!.Character.X = x;
            Player!.Character.Y = y;

            var instances = InstanceService!.Instances;
            var instanceId = Player!.Character.Map;

            if (instances.ContainsKey(instanceId)) {
                var instance = instances[instanceId];

                PacketSender!.SendPlayerMovement(Player, movement, instance);
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

        //// They tried to hack. 
        //if (moved = false && result == 1) {
        //    Warp(player.GetMap(), player.X, player.Y);
        //}
    }

    private bool CanMove(Direction direction) {
        var instances = InstanceService!.Instances;
        var instanceId = Player!.Character.Map;

        if (instances.ContainsKey(instanceId)) {
            var instance = instances[instanceId];
            var x = (int)Player!.Character.X;
            var y = (int)Player!.Character.Y;

            if (direction == Direction.UpLeft) {
                if (x > 0 && y > 0) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Left > 0) {
                        Warp(instance.Link.Left, instance.MaximumX, y);

                        return false;
                    }
                }
            }

            if (direction == Direction.UpRight) {
                if (x > 0 && x < instance.MaximumX) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Right > 0) {
                        Warp(instance.Link.Right, 0, y);

                        return false;
                    }
                }
            }

            if (direction == Direction.DownLeft) {
                if (y < instance.MaximumY && x > 0) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Left > 0) {
                        Warp(instance.Link.Left, instance.MaximumX, y);

                        return false;
                    }
                }
            }

            if (direction == Direction.DownRight) {
                if (y < instance.MaximumX && x < instance.MaximumY) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Right > 0) {
                        Warp(instance.Link.Right, 0, y);

                        return false;
                    }
                }
            }

            if (direction == Direction.Up) {
                if (y > 0) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Up > 0) {
                        Warp(instance.Link.Up, x, instance.MaximumY);

                        return false;
                    }
                }
            }

            if (direction == Direction.Down) {
                if (y < instance.MaximumY) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Down > 0) {
                        Warp(instance.Link.Down, x, 0);

                        return false;
                    }
                }
            }

            if (direction == Direction.Left) {
                if (x > 0) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Left > 0) {
                        Warp(instance.Link.Left, instance.MaximumX, y);

                        return false;
                    }
                }
            }

            if (direction == Direction.Right) {
                if (x < instance.MaximumX) {
                    if (CheckBlockedDirection(instance, direction)) {
                        return false;
                    }
                }
                else {
                    if (instance.Link.Right > 0) {
                        Warp(instance.Link.Right, 0, y);

                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool CheckBlockedDirection(IInstance instance, Direction direction) {
        var (x, y) = GetNextCoordinate(direction, (int)Player!.Character.X, (int)Player!.Character.Y);

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
        Direction.UpLeft => (x -= 1, y -= 1),
        Direction.UpRight => (x += 1, y -= 1),
        Direction.DownLeft => (x -= 1, y += 1),
        Direction.DownRight => (x += 1, y += 1),
        _ => (Invalid, Invalid)
    };

    private void Warp(int newInstanceId, int x, int y) {
        var instances = InstanceService!.Instances;

        if (instances.ContainsKey(newInstanceId)) {
            var instance = instances[newInstanceId];

            var warper = new WarperManager() {
                Player = Player,
                InstanceService = InstanceService,
                PacketSender = PacketSender
            };

            warper.Warp(instance, x, y);
        }
    }
}