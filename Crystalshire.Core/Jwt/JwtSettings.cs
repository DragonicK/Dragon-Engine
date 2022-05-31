namespace Crystalshire.Core.Jwt;

public class JwtSettings {
    public string SecurityKey { get; set; }
    public string DataSecurityKey { get; set; }
    public int ExpirationMinutes { get; set; }

    public JwtSettings() {
        SecurityKey = string.Empty;
        DataSecurityKey = string.Empty;
    }
}