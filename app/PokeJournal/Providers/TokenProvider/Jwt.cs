using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using PokeJournal.DTO;

namespace PokeJournal.Providers.TokenProvider;

public class Jwt : ITokenProvider
{
    public string Hash(UserDTO user)
    {
        string jwtSecretKey = Environment.GetEnvironmentVariable("JWY_SECRET_KEY") ?? throw new InvalidOperationException("\"JWT Sercret key\" Not founded. Pleace check the env variable");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, user.userName),
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
        }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenStr = tokenHandler.WriteToken(token);
        return tokenStr;
    }
}
