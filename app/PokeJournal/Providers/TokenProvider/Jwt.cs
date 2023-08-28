using System.Text;
using PokeJournal.DTO;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using DotNetEnv;

namespace PokeJournal.Providers.TokenProvider; 

public class Jwt : ITokenProvider{
  public Jwt(){
    Env.Load();
  } 

  public string Hash(UserDTO user){
    string jwtSecretKey = Environment.GetEnvironmentVariable("JWY_SECRET_KEY");

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(jwtSecretKey);

    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.userName),
            new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    var tokenStr = tokenHandler.WriteToken(token);
    return tokenStr;
  }
}
