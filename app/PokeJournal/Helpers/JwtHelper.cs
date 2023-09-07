using System.Security.Claims;
using PokeJournal.DTO;

namespace PokeJournal.Helpers;

public class JwtHelper{
    private readonly ClaimsIdentity _claimsIdentity;

    public JwtHelper(ClaimsIdentity claimsIdentity){
      _claimsIdentity = claimsIdentity;
    }

    public string? GetClaimValue(string claimType)
    {
       var claim = _claimsIdentity.Claims.FirstOrDefault(c => c.Type == claimType);

       if(claim == null){
         return null;
       }

       return claim.Value;
    }
}

