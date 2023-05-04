using Dragon.Core.Jwt;

using Dragon.Network;
 
using Dragon.Chat.Players;

namespace Dragon.Chat.Repository;

public interface IPlayerRepository {
    IPlayer Add(JwtTokenData jwtTokenData, IConnection connection);
    void Remove(IPlayer player);
    void Clear();
    IPlayer? FindByAccountId(long id);
    IPlayer? FindByCharacterId(long id);
    IPlayer? FindByName(string name);
    IPlayer? FindByConnection(IConnection connection);
    IPlayer? RemoveFromConnection(IConnection connection);
    IDictionary<int, IPlayer> GetPlayers();
}