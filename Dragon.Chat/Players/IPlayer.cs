using Dragon.Core.Model;

using Dragon.Network;

namespace Dragon.Chat.Players;

public interface IPlayer {
    IConnection Connection { get; set; }
    AccountLevel AccountLevel { get; }
    string Username { get; set; }
    string Name { get; set; }
    long AccountId { get; set; }
    long CharacterId { get; set; }
    int PartyId { get; set; }
    int LegionId { get; set; }
    int InstanceId { get; set; }
}