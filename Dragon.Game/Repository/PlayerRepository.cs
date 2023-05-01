using Dragon.Network;

using Dragon.Core.Jwt;

using Dragon.Game.Players;

namespace Dragon.Game.Repository;

public class PlayerRepository : IPlayerRepository {
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
            var key = player.GetConnection().Id;

            players.Remove(key);
        }
    }

    public void Clear() {
        players?.Clear();
    }

    public IPlayer? FindByUsername(string username) {
        return players
            .Select(pair => pair.Value)
            .FirstOrDefault(player => player.Username.CompareTo(username) == 0);
    }

    public IPlayer? FindByName(string name) {
        return players
            .Select(pair => pair.Value)
            .FirstOrDefault(player => player.Character.Name.CompareTo(name) == 0);
    }

    public IPlayer? FindByAccountId(long id) {
        return players
            .Select(pair => pair.Value)
            .FirstOrDefault(player => player.AccountId == id);
    }

    public IPlayer? FindByCharacterId(long id) {
        return players
            .Select(pair => pair.Value)
            .FirstOrDefault(player => player.Character.CharacterId == id);
    }

    public IPlayer? FindByConnectionId(int id) {
        return players
           .Select(pair => pair.Value)
           .FirstOrDefault(player => player.GetConnection().Id == id);
    }

    public IPlayer? RemoveFromConnectionId(int id) {
        var player = players
           .Select(pair => pair.Value)
           .FirstOrDefault(player => player.GetConnection().Id == id);

        Remove(player);

        return player;
    }

    public IDictionary<int, IPlayer> GetPlayers() {
        return players;
    }

}