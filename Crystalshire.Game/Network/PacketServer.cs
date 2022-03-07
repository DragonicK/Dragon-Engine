using Crystalshire.Core.Model;
using Crystalshire.Core.Network;
using Crystalshire.Core.Network.Messaging.SharedPackets;
using Crystalshire.Core.Network.Messaging.DTO;

using Crystalshire.Game.Players;

namespace Crystalshire.Game.Network {
    public sealed partial class PacketSender {

        public void SendPing(IPlayer player) {
            var packet = new PacketPing() {
                ClienteRequest = true
            };

            var msg = Writer.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendAlertMessage(IPlayer player, AlertMessageType alertMessage, MenuResetType resetType, bool kick = false, bool forced = false) {
            SendAlertMessage(player.GetConnection(), alertMessage, resetType, kick, forced);
        }

        public void SendAlertMessage(IConnection connection, AlertMessageType alertMessage, MenuResetType resetType, bool kick = false, bool forced = false) {
            var packet = new SpAlertMessage() {
                AlertMessage = alertMessage,
                MenuReset = resetType,
                Kick = kick,
                Forced = forced
            };

            var msg = Writer.CreateMessage(packet);

            msg.DestinationPeers.Add(connection.Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendServerConfiguration(IPlayer player) {
            var config = Configuration.Player;

            var packet = new SpServerConfiguration() {
                MaximumAuras = config.MaximumAuras,
                MaximumHeraldries = config.MaximumHeraldries,
                MaximumInventories = config.MaximumInventory,
                MaximumLevel = config.MaximumLevel,
                MaximumRecipes = config.MaximumRecipes,
                MaximumSkills = config.MaximumSkills,
                MaximumEffects = config.MaximumEffects,
                MaximumTitles = config.MaximumTitles,
                MaximumWarehouse = config.MaximumWarehouse,
                MaximumMails = config.MaximumMails
            };

            var msg = Writer.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendInGame(IPlayer player) {
            var packet = new SpInGame() {
                IsInGame = true
            };

            var msg = Writer.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }

        public void SendServerRates(IPlayer player) {
            const int Multiplier = 100;

            var rates = Configuration!.Rates;

            var packet = new SpServerRates() {
                Rates = new DataRate() {
                    Character = Convert.ToInt32(rates.Character * Multiplier),
                    Party = Convert.ToInt32(rates.Party * Multiplier),
                    Guild = Convert.ToInt32(rates.Guild * Multiplier),
                    Skill = Convert.ToInt32(rates.Skill * Multiplier),
                    Craft = Convert.ToInt32(rates.Craft * Multiplier),
                    Quest = Convert.ToInt32(rates.Quest * Multiplier),
                    Pet = Convert.ToInt32(rates.Pet * Multiplier),
                    GoldChance = Convert.ToInt32(rates.GoldChance * Multiplier),
                    GoldIncrease = Convert.ToInt32(rates.GoldIncrease * Multiplier),
                    ItemDrop = new int[] {
                        Convert.ToInt32(rates.ItemDrops[Rarity.Common] * Multiplier),
                        Convert.ToInt32(rates.ItemDrops[Rarity.Uncommon] * Multiplier),
                        Convert.ToInt32(rates.ItemDrops[Rarity.Rare] * Multiplier),
                        Convert.ToInt32(rates.ItemDrops[Rarity.Epic] * Multiplier),
                        Convert.ToInt32(rates.ItemDrops[Rarity.Mythic] * Multiplier),
                        Convert.ToInt32(rates.ItemDrops[Rarity.Ancient] * Multiplier),
                        Convert.ToInt32(rates.ItemDrops[Rarity.Legendary] * Multiplier),
                        Convert.ToInt32(rates.ItemDrops[Rarity.Ethereal] * Multiplier),  
                    }
                }
            };

            var msg = Writer!.CreateMessage(packet);

            msg.DestinationPeers.Add(player.GetConnection().Id);
            msg.TransmissionTarget = TransmissionTarget.Destination;

            Writer.Enqueue(msg);
        }
    }
}