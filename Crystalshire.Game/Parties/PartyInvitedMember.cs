using Crystalshire.Game.Players;

namespace Crystalshire.Game.Parties {
    public class PartyInvitedMember {
        public IPlayer? Player { get; set; }
        public int AcceptTimeOut { get; set; }
        public bool CouldBeRemoved { get; set; }
    }
}