using Dragon.Core.Jwt;
using Dragon.Core.Logs;
using Dragon.Core.Model;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Game.Network;
using Dragon.Game.Players;
using Dragon.Game.Services;
using Dragon.Game.Characters;
using Dragon.Game.Network.Senders;

namespace Dragon.Game.Routes;

public sealed class Authentication : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.GameServerLogin;

    private readonly JwtTokenHandler JwtHandler;
    private readonly ICharacterDatabase Database;

    public Authentication(IServiceInjector injector) : base(injector) {
        JwtHandler = new JwtTokenHandler(Configuration!.JwtSettings);
        Database = new CharacterDatabase(Configuration!, DatabaseService!.DatabaseFactory!);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpGameServerLogin;

        if (received is not null) {
            Process(connection, received);
        }
    }

    private void Process(IConnection connection, CpGameServerLogin packet) {
        var logger = GetLogger();
        var sender = GetPacketSender();

        var jwtToken = packet.Token;
        var jwtTokenData = JwtHandler.Validate(jwtToken);

        if (jwtTokenData.AccountId > 0) {
            Authenticate(logger, sender, connection, jwtTokenData);
        }
        else {
            logger.Warning(GetType().Name, $"Authentication Failed {jwtToken}");

            Disconnect(sender, connection, AlertMessageType.Connection);
        }
    }

    private async void Authenticate(ILogger logger, IPacketSender sender, IConnection connection, JwtTokenData jwtTokenData) {
        var accountId = jwtTokenData.AccountId;
        var username = jwtTokenData.Username;

        var player = FindDuplicated(accountId);
        var repository = GetPlayerRepository();

        logger.Warning(GetType().Name, $"Authenticated {username}");

        if (player is not null) {
            Disconnect(sender, connection, AlertMessageType.DuplicatedLogin);
            Disconnect(sender, player.Connection, AlertMessageType.TryingToLogin);

            logger.Error(GetType().Name, $"Duplicated Entry {username}");

            // Wait about 1 second before disconnect.
            await Task.Delay(1000);

            player.Connection.Disconnect();

            repository.Remove(player);
        }
        else {
            player = repository.Add(jwtTokenData, connection);

            try {
                player.Characters = await Database.GetCharactersPreviewAsync(accountId);
            }
            catch (Exception ex) {
                await WriteExceptionLog(logger, username, ex.Message);
            }

            if (player.Characters is not null) {
                AddDeleteRequest(player);

                sender.SendCharacters(player);

                logger.Info(GetType().Name, $"Sending characters User: {username}");
            }
            else {
                logger.Info(GetType().Name, $"Failed to load characterrs User: {username}");
            }
        }
    }

    private IPlayer? FindDuplicated(long accountId) {
        return GetPlayerRepository().FindByAccountId(accountId);
    }

    private void AddDeleteRequest(IPlayer player) {
        var characters = player.Characters.ToList();

        characters.ForEach(character => { 
            if (character.DeleteRequest is not null) {
                if (!CharacterService!.IsAdded(character.CharacterId)) {
                    CharacterService.AddExclusion(character.DeleteRequest); 
                } 
            } 
        });
    }

    private void Disconnect(IPacketSender sender, IConnection connection, AlertMessageType alertMessage) {
        sender.SendAlertMessage(connection, alertMessage, MenuResetType.None, true);
    }

    private Task WriteExceptionLog(ILogger logger, string username, string message) {
        logger.Error(GetType().Name, $"Authentication: An error ocurred by {username} ... {message}");

        return Task.CompletedTask;
    }
}