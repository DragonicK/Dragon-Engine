using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Dragon.Core.Jwt;

public class JwtTokenHandler {
    private readonly int minutes;
    private readonly string securityKey;
    private readonly string dataSecurityKey;

    public JwtTokenHandler(JwtSettings jwtSettings) {
        minutes = jwtSettings.ExpirationMinutes;
        securityKey = jwtSettings.SecurityKey;
        dataSecurityKey = jwtSettings.DataSecurityKey;
    }

    public string GerenateToken(JwtTokenData jwtToken) {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.securityKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var handler = new JwtSecurityTokenHandler();

        var claims = new ClaimsIdentity(new Claim[]
            {
                    new Claim("Username", jwtToken.Username),
                    new Claim("AccountId", jwtToken.AccountId.ToString()),
                    new Claim("CharacterId", jwtToken.CharacterId.ToString()),
                    new Claim("AccountLevel", jwtToken.AccountLevel.ToString())
            }
        );

        var descriptor = new SecurityTokenDescriptor {
            Subject = claims,
            Expires = DateTime.UtcNow.AddMinutes(minutes),
            NotBefore = DateTime.UtcNow,
            SigningCredentials = credentials
        };

        var token = handler.CreateJwtSecurityToken(descriptor);

        return handler.WriteToken(token);
    }

    public JwtTokenData Validate(string token) {
        var handler = new JwtSecurityTokenHandler();

        var validationParameters = new TokenValidationParameters {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey)),
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateActor = false,
            ValidateIssuer = false
        };

        ClaimsPrincipal? claims = null;

        try {
            claims = handler.ValidateToken(token, validationParameters, out var securityToken);
        }
        catch {

        }

        return GetJwtTokenData(claims);
    }

    private JwtTokenData GetJwtTokenData(ClaimsPrincipal? claims) {
        if (claims is null) {
            return new JwtTokenData();
        }

        var username = claims.FindFirst(x => x.Type == "Username");
        var accountId = claims.FindFirst(x => x.Type == "AccountId");
        var characterId = claims.FindFirst(x => x.Type == "CharacterId");
        var accountLevel = claims.FindFirst(x => x.Type == "AccountLevel");

        return new JwtTokenData() {
            Username = username is not null ? username.Value : String.Empty,
            AccountId = accountId is not null ? int.Parse(accountId.Value) : 0,
            CharacterId = characterId is not null ? int.Parse(characterId.Value) : 0,
            AccountLevel = accountLevel is not null ? int.Parse(accountLevel.Value) : 0,
        };
    }
}