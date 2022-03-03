using Crystalshire.Core.Jwt;
using Crystalshire.Core.Logs;
using Crystalshire.Core.Network;
using Crystalshire.Core.Model;
using Crystalshire.Core.Network.Messaging.SharedPackets;

using Crystalshire.Game.Network;
using Crystalshire.Game.Services;
using Crystalshire.Game.Players;
using Crystalshire.Game.Repository;
using Crystalshire.Game.Characters;

namespace Crystalshire.Game.Routes {
    public sealed class Authentication {
        public IConnection? Connection { get; set; }
        public CpGameServerLogin? Packet { get; set; }
        public PacketSenderService? PacketSenderService { get; init; }
        public CharacterService? CharacterService { get; init; }
        public ConfigurationService? Configuration { get; init; }
        public ConnectionService? ConnectionService { get; init; }
        public DatabaseService? DatabaseService { get; init; }
        public LoggerService? LoggerService { get; init; }

        public async void Process() {
            var jwtToken = Packet.Token;
            var jwtTokenData = GetValidatedData(jwtToken);

            if (jwtTokenData.AccountId == 0) {
                Disconnect(Connection, AlertMessageType.Connection);
                WriteOutputLog($"Authentication Failed {jwtToken}");

                return;
            }

            var accountId = jwtTokenData.AccountId;
            var username = jwtTokenData.Username;

            var player = FindDuplicated(accountId);
            var repository = GetPlayerRepository();

            WriteOutputLog($"Authenticated {username} {jwtToken}");

            if (player is not null) {
                Disconnect(Connection, AlertMessageType.DuplicatedLogin);
                Disconnect(player.GetConnection(), AlertMessageType.TryingToLogin);

                WriteOutputLog($"Duplicated Entry {username} {jwtToken}");

                // Wait about 1 second before disconnect.
                await Task.Delay(1000);

                player.GetConnection().Disconnect();

                repository!.Remove(player);
            }
            else {
                player = repository.Add(jwtTokenData, Connection);

                try {
                    var database = new CharacterDatabase(Configuration, DatabaseService.DatabaseFactory);

                    player.Characters = await database.GetCharactersPreviewAsync(accountId);

                    database?.Dispose();
                }
                catch (Exception ex) {
                    await WriteExceptionLog(username, ex.Message);
                }

                if (player.Characters is not null) {
                    AddDeleteRequest(player);

                    var sender = GetSender();
                    sender?.SendCharacters(player);

                    WriteOutputLog($"Sending Characters -> {username}");
                }
                else {
                    WriteOutputLog("Failed to load characters");
                }
            }
        }

        private ILogger? GetLogger() {
            return LoggerService?.ServerLogger;
        }

        private IPlayerRepository? GetPlayerRepository() {
            return ConnectionService?.PlayerRepository;
        }

        private JwtTokenData GetValidatedData(string jwtToken) {
            var handler = new JwtTokenHandler(Configuration.JwtSettings);

            return handler.Validate(jwtToken);
        }

        private IPlayer? FindDuplicated(long accountId) {
            return ConnectionService?.PlayerRepository?.FindByAccountId(accountId);
        }

        private void WriteOutputLog(string log) {
            if (Configuration is not null) {
                if (Configuration.Debug) {
                    OutputLog.Write(log);
                }
            }
        }

        private void AddDeleteRequest(IPlayer player) {
            var characters = player.Characters.ToList();

            characters?.ForEach(character => {
                if (character.DeleteRequest is not null) {
                    if (!CharacterService.IsAdded(character.CharacterId)) {
                        CharacterService.AddExclusion(character.DeleteRequest);
                    }
                }
            });
        }

        private void Disconnect(IConnection connection, AlertMessageType alertMessage) {
            var sender = GetSender();
            sender?.SendAlertMessage(connection, alertMessage, MenuResetType.None, true);
        }

        private Task WriteExceptionLog(string username, string message) {
            var logger = GetLogger();

            var description = new Description() {
                Name = "Authentication Excpetion",
                WarningCode = WarningCode.Error,
                Message = $"An error ocurred by {username} ... {message}",
            };

            logger?.Write(description);

            OutputLog.Write($"Authentication throw an exception ... ");

            return Task.CompletedTask;
        }

        private IPacketSender? GetSender() {
            return PacketSenderService?.PacketSender;
        }
    }
}
