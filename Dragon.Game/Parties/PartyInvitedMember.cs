using Dragon.Game.Players;

namespace Dragon.Game.Parties;

public class PartyInvitedMember {
    public IPlayer? Player { get; set; }
    public int AcceptTimeOut { get; set; }
    public bool CouldBeRemoved { get; set; }
}