using Dragon.Network;
using Dragon.Core.Jwt;
using Dragon.Game.Players;

namespace Dragon.Game.Repository;

public interface IPlayerRepository {
    IPlayer Add(JwtTokenData jwtTokenData, IConnection connection);
    void Remove(IPlayer player);
    void Clear();

    IPlayer? FindByAccountId(long id);
    IPlayer? FindByCharacterId(long id);
    IPlayer? FindByUsername(string username);
    IPlayer? FindByName(string name);
    IPlayer? FindByConnectionId(int id);
    IPlayer? RemoveFromConnectionId(int connectionId);
    IDictionary<int, IPlayer> GetPlayers();
}