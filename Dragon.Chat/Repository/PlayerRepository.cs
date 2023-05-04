using Dragon.Core.Jwt;

using Dragon.Network;

using Dragon.Chat.Players;

namespace Dragon.Chat.Repository;

public sealed class PlayerRepository : IPlayerRepository {
    private readonly Dictionary<int, IPlayer> players;

    public PlayerRepository(int capacity) {
        players = new Dictionary<int, IPlayer>(capacity);
    }

    public IPlayer Add(JwtTokenData jwtTokenData, IConnection connection) {
        var player = new Player(
            jwtTokenData.Username,
            jwtTokenData.AccountId,
            jwtTokenData.AccountLevel,
            connection);

        players.Add(connection.Id, player);

        return player;
    }

    public void Remove(IPlayer? player) {
        if (player is not null) {
            var key = player.Connection.Id;

            players.Remove(key);
        }
    }

    public void Clear() {
        players?.Clear();
    }

    public IPlayer? FindByName(string name) {
        return players
            .Select(pair => pair.Value)
            .FirstOrDefault(player => player.Name.CompareTo(name) == 0);
    }

    public IPlayer? FindByAccountId(long id) {
        return players
            .Select(pair => pair.Value)
            .FirstOrDefault(player => player.AccountId == id);
    }

    public IPlayer? FindByCharacterId(long id) {
        return players
            .Select(pair => pair.Value)
            .FirstOrDefault(player => player.CharacterId == id);
    }

    public IPlayer? FindByConnection(IConnection connection) {
        return players
           .Select(pair => pair.Value)
           .FirstOrDefault(player => player.Connection == connection);
    }

    public IPlayer? RemoveFromConnection(IConnection connection) {
        var player = players
           .Select(pair => pair.Value)
           .FirstOrDefault(player => player.Connection == connection);

        Remove(player);

        return player;
    }

    public IDictionary<int, IPlayer> GetPlayers() {
        return players;
    }
}