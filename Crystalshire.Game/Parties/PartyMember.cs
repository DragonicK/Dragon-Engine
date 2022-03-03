using Crystalshire.Game.Players;

namespace Crystalshire.Game.Parties {
    public class PartyMember {
        public int Index { get; set; }
        public int Model { get; set; }
        public long CharacterId { get; set; }
        public string Character { get; set; }
        public bool Disconnected { get; set; }
        public IPlayer? Player { get; set; }
        public int DisconnectionTimeOut { get; set; }

        public PartyMember() {
            Character = string.Empty;
        }

        public void Clear() {
            Index = 0;
            Model = 0;
            CharacterId = 0;
            Character = string.Empty;
            Disconnected = false;
            Player = default;
        }

        public PartyMember Clone() {
            var clone = (PartyMember)MemberwiseClone();

            clone.Player = Player;

            return clone;
        }
    }
}