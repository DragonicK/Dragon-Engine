using Dragon.Core.Model;

using Dragon.Network;

namespace Dragon.Chat.Players;

public sealed class Player : IPlayer {
    public IConnection Connection { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public long AccountId { get; set; }
    public long CharacterId { get; set; }
    public int PartyId { get; set; }
    public int LegionId { get; set; }
    public int InstanceId { get; set; }
    public AccountLevel AccountLevel { get; }

    public Player(string username, string character, long characterId, long accountId, int accessLevel, IConnection connection) {
        Username = username;
        Name = character;
        CharacterId = characterId;
        AccountId = accountId;
        Connection = connection;
        AccountLevel = (AccountLevel)accessLevel;
    }
}