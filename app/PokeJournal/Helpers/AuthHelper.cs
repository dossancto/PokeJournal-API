using System.Security.Claims;

namespace PokeJournal.Helpers;

public abstract class AuthHelper{

    public static Guid UserId(ClaimsPrincipal user){
        var token = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if(!Guid.TryParse(token, out var id) || id == Guid.Empty){
            throw new Exception("Invalid user token");
        }

        return id;

    }
}