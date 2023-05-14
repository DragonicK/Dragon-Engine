using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Game.Parties;
using Dragon.Game.Players;
using Dragon.Game.Services;

namespace Dragon.Game.Manager;

public sealed class AuraManager {
    public ContentService? ContentService { get; private set; }
    public InstanceService? InstanceService { get; private set; }
    public PacketSenderService? PacketSenderService { get; private set; }

    private readonly GiveEffectManager EffectManager;

    public AuraManager(IServiceInjector injector) {
        injector.Inject(this);

        EffectManager = new GiveEffectManager(injector);
    }

    public void CheckPartyMemberAuras(IPlayer player) {
        var party = GetPartyManager(player);

        if (party is not null) {
            var auras = player.Auras.ToList();

            var x = player.Character.X;
            var y = player.Character.Y;

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
                                    ApplyEffect(range, id, level, x, y, member);
                                }
                                else {
                                    if (member.Player.Effects.Contains(id)) {
                                        RemoveEffect(id, member);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void ActivateAura(IPlayer player, int id, int level, int range) {
        var party = GetPartyManager(player);

        if (party is not null) {
            var members = party.Members;

            var x = player.Character.X;
            var y = player.Character.Y;

            foreach (var member in members) {
                ApplyEffect(range, id, level, x, y, member);
            }
        }
        else {
            EffectManager.GiveEffect(player, id, level, 0, true);
        }
    }

    public void DeactivateAura(IPlayer player, int id) {
        var party = GetPartyManager(player);

        if (party is not null) {
            var members = party.Members;

            foreach (var member in members) {
                RemoveEffect(id, member);
            }
        }
        else {
            EffectManager.RemoveEffect(player, id);
        }
    }

    private void ApplyEffect(int range, int id, int level, int x, int y, PartyMember member) {
        if (!member.Disconnected) {
            var player = member.Player;

            if (player is not null) {
                if (player.Vitals.Get(Vital.HP) > 0) {
                    var x2 = player.Character.X;
                    var y2 = player.Character.Y;

                    if (!player.Effects.Contains(id)) {
                        if (IsInRange(range, x, y, x2, y2)) {
                            EffectManager.GiveEffect(player, id, level, 0, true);
                        }
                    }
                }
            }
        }
    }

    private void RemoveEffect(int id, PartyMember member) {
        if (!member.Disconnected) {
            var player = member.Player;

            if (player is not null) {
                if (player.Effects.Contains(id)) {
                    EffectManager.RemoveEffect(player, id);
                }
            }
        }
    }

    private bool IsInRange(int range, int x1, int y1, int x2, int y2) {
        return Convert.ToInt32(Math.Sqrt(Math.Pow((x1 - x2), 2) + Math.Pow((y1 - y2), 2))) <= range;
    }

    private Party? GetPartyManager(IPlayer player) {
        var id = player.PartyId;
        var parties = InstanceService!.Parties;

        parties.TryGetValue(id, out var party);

        return party;
    }
}