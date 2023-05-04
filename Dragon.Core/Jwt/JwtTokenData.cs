namespace Dragon.Core.Jwt;

public sealed class JwtTokenData {
    public string Username { get; set; }
    public string Character { get; set; }
    public long AccountId { get; set; }
    public long CharacterId { get; set; }
    public int AccountLevel { get; set; }

    public JwtTokenData() {
        Username = string.Empty;
        Character = string.Empty;
    }
}