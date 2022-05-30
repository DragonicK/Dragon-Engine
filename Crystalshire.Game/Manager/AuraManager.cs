using Crystalshire.Core.Content;
using Crystalshire.Core.Model.Effects;

using Crystalshire.Game.Parties;
using Crystalshire.Game.Players;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;

namespace Crystalshire.Game.Manager;

public class AuraManager {
    public IPlayer? Player { get; init; }
    public IDatabase<Effect>? Effects { get; init; }
    public IPacketSender? PacketSender { get; init; }
    public InstanceService? InstanceService { get; init; }

    public void CheckPartyMemberAuras() {
        var party = GetPartyManager();

        if (party is not null) {
            var auras = Player!.Auras.ToList();

            var x = Player!.Character.X;
            var y = Player!.Character.Y;

            var manager = new GiveEffectManager() {
                Effects = Effects,
                PacketSender = PacketSender,
                InstanceService = InstanceService
            };

            foreach (var aura in auras) {
                var id = aura.Id;
                var level = aura.Level;
                var range = aura.Range;

                if (aura.Id > 0) {
                    var members = party.Members;

                    foreach (var member in members) {
                        if (!member.Disconnected) {
                            if (member.Player is not null) {
                                var x2 = member.Player.Character.X;
                                var y2 = member.Player.Character.Y;

                                if (IsInRange(range, x, y, x2, y2)) {
                                    ApplyEffect(range, id, level, x, y, manager, member);
                                }
                                else {
                                    if (member.Player.Effects.Contains(id)) {
                                        RemoveEffect(id, manager, member);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void ActivateAura(int id, int level, int range) {
        var party = GetPartyManager();

        var manager = new GiveEffectManager() {
            Effects = Effects,
            PacketSender = PacketSender,
            InstanceService = InstanceService
        };

        if (party is not null) {
            var members = party.Members;

            var x = Player!.Character.X;
            var y = Player!.Character.Y;

            foreach (var member in members) {
                ApplyEffect(range, id, level, x, y, manager, member);
            }
        }
        else {
            manager.GiveEffect(Player!, id, level, 0, true);
        }
    }

    public void DeactivateAura(int id) {
        var party = GetPartyManager();

        var manager = new GiveEffectManager() {
            Effects = Effects,
            PacketSender = PacketSender,
            InstanceService = InstanceService
        };

        if (party is not null) {
            var members = party.Members;

            foreach (var member in members) {
                RemoveEffect(id, manager, member);
            }
        }
        else {
            manager.RemoveEffect(Player!, id);
        }
    }

    private void ApplyEffect(int range, int id, int level, int x, int y, GiveEffectManager manager, PartyMember member) {
        if (!member.Disconnected) {
            var player = member.Player;

            if (player is not null) {
                var x2 = player.Character.X;
                var y2 = player.Character.Y;

                if (!player.Effects.Contains(id)) {
                    if (IsInRange(range, x, y, x2, y2)) {
                        manager.GiveEffect(player, id, level, 0, true);
                    }
                }
            }
        }
    }

    private void RemoveEffect(int id, GiveEffectManager manager, PartyMember member) {
        if (!member.Disconnected) {
            var player = member.Player;

            if (player is not null) {

                if (player.Effects.Contains(id)) {
                    manager.RemoveEffect(player, id);
                }
            }
        }
    }

    private bool IsInRange(int range, int x1, int y1, int x2, int y2) {
        var r = Convert.ToInt32(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2)));
        return r <= range;
    }

    private PartyManager? GetPartyManager() {
        var id = Player!.PartyId;
        var parties = InstanceService!.Parties;

        if (parties.ContainsKey(id)) {
            return parties[id];
        }

        return null;
    }
}