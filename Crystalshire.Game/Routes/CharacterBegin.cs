using Crystalshire.Core.Logs;
using Crystalshire.Core.Model;
using Crystalshire.Core.Network;
using Crystalshire.Core.Model.Maps;
using Crystalshire.Core.Network.Outgoing;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Server;
using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Players;
using Crystalshire.Game.Repository;
using Crystalshire.Game.Characters;

namespace Crystalshire.Game.Routes {
    public sealed class CharacterBegin {
        public IConnection? Connection { get; set; }
        public CpCharacterBegin? Packet { get; set; }
        public LoggerService? LoggerService { get; init; }
        public ContentService? ContentService { get; init; }
        public InstanceService? InstanceService { get; init; }
        public DatabaseService? DatabaseService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public PacketSenderService? PacketSenderService { get; init; }

        public async void Process() {
            var index = Packet.CharacterIndex;

            var repository = GetPlayerRepository();
            var player = repository.FindByConnectionId(Connection.Id);

            if (index < Configuration.Character.Maximum) {
                if (player.Characters is not null) {
                    var count = player.Characters.Count;

                    if (index < count) {
                        var result = await LoadPlayer(player, index);

                        if (result) {
                            JoinGame(player);
                        }
                    }
                }
            }
            else {
                var sender = GetSender();
                sender?.SendAlertMessage(player, AlertMessageType.Failed, MenuResetType.Characters);
            }
        }

        private async Task<bool> LoadPlayer(IPlayer player, int index) {
            var characterId = player.Characters[index].CharacterId;
            var result = false;

            ICharacterDatabase? database = null;

            try {
                database = new CharacterDatabase(Configuration, DatabaseService.DatabaseFactory);

                result = await database.LoadCharacterAsync(characterId, player);
            }
            catch (Exception ex) {
                await WriteExceptionLog(player.Username, ex.Message);
            }
            finally {
                database?.Dispose();
            }

            return result;
        }

        private void JoinGame(IPlayer player) {
            var game = new JoinGame() {
                Player = player,
                Logger = GetLogger(),
                PacketSender = GetSender(),
                Configuration = Configuration,
                InstanceService = InstanceService,
                ContentService = ContentService
            };

            game.Join();
        }

        private IPlayerRepository? GetPlayerRepository() {
            return ConnectionService?.PlayerRepository;
        }

        private ILogger? GetLogger() {
            return LoggerService?.ServerLogger;
        }
        
        private MapPassphrase? GetMapPassphrase() {
            return InstanceService?.Passphrases;
        }

        private Task WriteExceptionLog(string username, string message) {
            var logger = GetLogger();

            var description = new Description() {
                Name = "Character Load Excpetion",
                WarningCode = WarningCode.Error,
                Message = $"An error ocurred by {username} ... {message}",
            };

            logger?.Write(description);

            OutputLog.Write($"Character Load throw an exception ... ");

            return Task.CompletedTask;
        }

        private IPacketSender? GetSender() {
            return PacketSenderService?.PacketSender;
        }
    }
}
