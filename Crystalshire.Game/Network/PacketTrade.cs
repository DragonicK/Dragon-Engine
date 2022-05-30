using Crystalshire.Network;
using Crystalshire.Network.Outgoing;
using Crystalshire.Network.Messaging.DTO;
using Crystalshire.Network.Messaging.SharedPackets;

using Crystalshire.Core.Model;
using Crystalshire.Core.Model.Maps;
using Crystalshire.Core.Model.DisplayIcon;
using Crystalshire.Core.Model.Characters;

using Crystalshire.Game.Manager;
using Crystalshire.Game.Players;
using Crystalshire.Game.Services;
using Crystalshire.Game.Messages;
using Crystalshire.Game.Instances;
using Crystalshire.Game.Configurations;

namespace Crystalshire.Game.Network;

public sealed partial class PacketSender {
    public void SendTradeInvite(IPlayer starter, IPlayer invited) {
        var packet = new SpTradeInvite() {
            Name = starter.Character.Name
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(invited.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendCloseTrade(IPlayer player) {
        var packet = new SpCloseTrade();

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendOpenTrade(IPlayer player, string text, string otherText, string whoStartedName) {
        var packet = new SpOpenTrade() {
            Text = text,
            InvitedText = otherText,
            Name = whoStartedName
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendTradeMyInventory(IPlayer player, int index, int inventory, int amount) {
        var packet = new SpTradeMyInventory() {
            Index = index,
            Inventory = inventory,
            Amount = amount
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendTradeOtherInventory(IPlayer player, CharacterInventory inventory) {
        var packet = new SpTradeOtherInventory() {
            Inventory = new DataInventory() {
                Index = inventory.InventoryIndex,
                Id = inventory.ItemId,
                Value = inventory.Value,
                Level = inventory.Level,
                Bound = inventory.Bound,
                AttributeId = inventory.AttributeId,
                UpgradeId = inventory.UpgradeId
            }
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendTradeState(IPlayer player, TradeState state, TradeState myState, TradeState otherState) {
        var packet = new SpTradeState() {
            State = state,
            MyState = myState,
            OtherState = otherState
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

    public void SendTradeCurrency(IPlayer player, int starterAmount, int invitedAmount) {
        var packet = new PacketTradeCurrency() {
            StarterAmount = starterAmount,
            InvitedAmount = invitedAmount
        };

        var msg = Writer!.CreateMessage(packet);

        msg.DestinationPeers.Add(player.GetConnection().Id);
        msg.TransmissionTarget = TransmissionTarget.Destination;

        Writer.Enqueue(msg);
    }

}