using Crystalshire.Network;
using Crystalshire.Core.Jwt;
using Crystalshire.Game.Players;

namespace Crystalshire.Game.Repository;

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