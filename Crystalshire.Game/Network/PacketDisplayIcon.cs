using Crystalshire.Network;
using Crystalshire.Network.Messaging.DTO;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Core.Model.Entity;
using Crystalshire.Core.Model.DisplayIcon;

using Crystalshire.Game.Players;
using Crystalshire.Game.Instances;

namespace Crystalshire.Game.Network;

public sealed partial class PacketSender {

    public void SendDisplayIcon(ref DisplayIcon display, DisplayIconTarget target, IEntity entity, IInstance instance) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var packet = new SpDisplayIcon() {
            Index = entity.IndexOnInstance,
            DisplayTarget = target,
            Icon = new DataDisplayIcon() {
                IconType = display.Type,
                ExhibitionType = display.ExhibitionType,
                DurationType = display.DurationType,
                OperationType = display.OperationType,
                SkillType = display.SkillType,
                Id = display.Id,
                Level = display.Level,
                Duration = display.Duration
            }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);

        if (entity is IPlayer player) {
            SendPartyDisplayIcon(packet, player);
        }
    }

    public void SendDisplayIcons(IList<DisplayIcon> display, DisplayIconTarget target, IEntity entity, IInstance instance) {
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var packet = new SpDisplayIcons() {
            Index = entity.IndexOnInstance,
            DisplayTarget = target,
            Icons = new DataDisplayIcon[display.Count],
        };

        for (var i = 0; i < display.Count; ++i) {
            packet.Icons[i] = new DataDisplayIcon() {
                IconType = display[i].Type,
                ExhibitionType = display[i].ExhibitionType,
                DurationType = display[i].DurationType,
                OperationType = display[i].OperationType,
                SkillType = display[i].SkillType,
                Id = display[i].Id,
                Level = display[i].Level,
                Duration = display[i].Duration
            };
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);

        if (entity is IPlayer player) {
            SendPartyDisplayIcons(packet, player);
        }
    }

    public void SendDisplayIcons(IPlayer player, IInstance instance) {
        var effects = player.Effects.ToList();
        var players = instance.GetPlayers();
        var list = players.Select(p => p.GetConnection().Id);

        var packet = new SpDisplayIcons() {
            Index = player.IndexOnInstance,
            DisplayTarget = DisplayIconTarget.Player,
            Icons = new DataDisplayIcon[effects.Count],
        };

        for (var i = 0; i < effects.Count; ++i) {
            packet.Icons[i] = new DataDisplayIcon() {
                IconType = DisplayIconType.Effect,
                ExhibitionType = DisplayIconExhibition.Player,
                DurationType = DisplayIconDuration.Limited,
                OperationType = DisplayIconOperation.Update,
                SkillType = DisplayIconSkill.None,
                Id = effects[i].EffectId,
                Level = effects[i].EffectLevel,
                Duration = effects[i].EffectDuration
            };

            if (effects[i].IsAura) {
                packet.Icons[i].DurationType = DisplayIconDuration.Unlimited;
            }
        }

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.AddRange(list);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);

        SendPartyDisplayIcons(packet, player);
    }

    public void SendPartyDisplayIcons(IPlayer player) {
        var party = GetPartyManager(player);

        if (party is not null) {
            var effects = player.Effects.ToList();

            var packet = new SpDisplayIcons() {
                Index = party.GetIndex(player),
                DisplayTarget = DisplayIconTarget.Party,
                Icons = new DataDisplayIcon[effects.Count],
            };

            for (var i = 0; i < effects.Count; ++i) {
                packet.Icons[i] = new DataDisplayIcon() {
                    IconType = DisplayIconType.Effect,
                    ExhibitionType = DisplayIconExhibition.Party,
                    DurationType = DisplayIconDuration.Limited,
                    OperationType = DisplayIconOperation.Update,
                    SkillType = DisplayIconSkill.None,
                    Id = effects[i].EffectId,
                    Level = effects[i].EffectLevel,
                    Duration = effects[i].EffectDuration
                };

                if (effects[i].IsAura) {
                    packet.Icons[i].DurationType = DisplayIconDuration.Unlimited;
                }
            }

            var list = party.GetConnectionIds();

            if (list is not null) {
                var msg = Writer!.CreateMessage(packet);

                msg.DestinationPeers.AddRange(list);
                msg.TransmissionTarget = TransmissionTarget.Destination;

                Writer.Enqueue(msg);
            }
        }
    }

    private void SendPartyDisplayIcons(SpDisplayIcons packet, IPlayer player) {
        var party = GetPartyManager(player);

        if (party is not null) {
            packet.DisplayTarget = DisplayIconTarget.Party;
            packet.Index = party.GetIndex(player);

            var list = party.GetConnectionIds();

            if (list is not null) {
                var msg = Writer!.CreateMessage(packet);

                msg.DestinationPeers.AddRange(list);
                msg.TransmissionTarget = TransmissionTarget.Destination;

                Writer.Enqueue(msg);
            }
        }
    }

    private void SendPartyDisplayIcon(SpDisplayIcon packet, IPlayer player) {
        var party = GetPartyManager(player);

        if (party is not null) {
            packet.Index = party.GetIndex(player);
            packet.DisplayTarget = DisplayIconTarget.Party;

            var list = party.GetConnectionIds();

            if (list is not null) {
                var msg = Writer!.CreateMessage(packet);

                msg.DestinationPeers.AddRange(list);
                msg.TransmissionTarget = TransmissionTarget.Destination;

                Writer.Enqueue(msg);
            }
        }
    }
}