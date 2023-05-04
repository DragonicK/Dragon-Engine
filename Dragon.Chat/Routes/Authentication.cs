using Dragon.Core.Jwt;
using Dragon.Core.Logs;
using Dragon.Core.Services;

using Dragon.Network;
using Dragon.Network.Messaging;
using Dragon.Network.Messaging.SharedPackets;

using Dragon.Chat.Players;
using Dragon.Chat.Network;

namespace Dragon.Chat.Routes;

public sealed class Authentication : PacketRoute, IPacketRoute {
    public MessageHeader Header => MessageHeader.ChatServerLogin;

    private readonly JwtTokenHandler JwtHandler;

    public Authentication(IServiceInjector injector) : base(injector) {
        JwtHandler = new JwtTokenHandler(Configuration!.JwtSettings);
    }

    public void Process(IConnection connection, object packet) {
        var received = packet as CpChatServerLogin;

        if (received is not null) {
            Process(connection, received);
        }
    }

    private void Process(IConnection connection, CpChatServerLogin packet) {
        var logger = GetLogger();

        var jwtToken = packet.Token;
        var jwtTokenData = JwtHandler.Validate(jwtToken);

        if (jwtTokenData.AccountId > 0) {
            Authenticate(logger, connection, jwtTokenData);
        }
        else {
            logger.Warning(GetType().Name, $"Authentication Failed {jwtToken}");

            Disconnect(connection);
        }
    }

    private async void Authenticate(ILogger logger, IConnection connection, JwtTokenData jwtTokenData) {
        var accountId = jwtTokenData.AccountId;
        var username = jwtTokenData.Username;

        var player = FindDuplicated(accountId);
        var repository = GetPlayerRepository();

        logger.Warning(GetType().Name, $"Authenticated {username}");

        if (player is not null) {
            logger.Error(GetType().Name, $"Duplicated Entry {username}");

            player.Connection.Disconnect();

            // Wait about 1 second before disconnect.
            await Task.Delay(1000);

            repository.Remove(player);
        }
        else {
            repository.Add(jwtTokenData, connection); 
        }
    }

    private IPlayer? FindDuplicated(long accountId) {
        return GetPlayerRepository().FindByAccountId(accountId);
    }

    private void Disconnect(IConnection connection) {
        connection.Disconnect();
    }
}