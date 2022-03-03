namespace Crystalshire.Core.Jwt {
    public class JwtTokenData {
        public string Username { get; set; }
        public long AccountId { get; set; }
        public long CharacterId { get; set; }
        public int AccountLevel { get; set; }

        public JwtTokenData() {
            Username = string.Empty;
        }
    }
}