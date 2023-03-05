using Dragon.Network;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Core.Jwt;
using Dragon.Core.Logs;
using Dragon.Core.Model;

using Dragon.Game.Network;
using Dragon.Game.Services;
using Dragon.Game.Players;
using Dragon.Game.Repository;
using Dragon.Game.Characters;

namespace Dragon.Game.Routes;

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
        var logger = GetLogger();
        var jwtToken = Packet!.Token;
        var jwtTokenData = GetValidatedData(jwtToken);

        if (jwtTokenData.AccountId == 0) {
            Disconnect(Connection!, AlertMessageType.Connection);

            logger?.Warning(GetType().Name, $"Authentication Failed {jwtToken}");

            return;
        }

        var accountId = jwtTokenData.AccountId;
        var username = jwtTokenData.Username;

        var player = FindDuplicated(accountId);
        var repository = GetPlayerRepository();

        logger?.Warning(GetType().Name, $"Authenticated {username}");

        if (player is not null) {
            Disconnect(Connection!, AlertMessageType.DuplicatedLogin);
            Disconnect(player.GetConnection(), AlertMessageType.TryingToLogin);

            logger?.Error(GetType().Name, $"Duplicated Entry {username}");

            // Wait about 1 second before disconnect.
            await Task.Delay(1000);

            player.GetConnection().Disconnect();

            repository!.Remove(player);
        }
        else {
            player = repository!.Add(jwtTokenData, Connection!);

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

                logger?.Info(GetType().Name, $"Sending characters User: {username}");
            }
            else {
                logger?.Info(GetType().Name, $"Failed to load characterrs User: {username}");
            }
        }
    }

    private ILogger? GetLogger() {
        return LoggerService?.Logger;
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

        logger?.Error(GetType().Name, $"Authentication: An error ocurred by {username} ... {message}");

        return Task.CompletedTask;
    }

    private IPacketSender? GetSender() {
        return PacketSenderService?.PacketSender;
    }
}