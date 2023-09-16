using System.Security.Claims;

using PokeJournal.Data;

using PokeJournal.Helpers;

using User = PokeJournal.Usecases.User;

namespace PokeJournal.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ApplicationDbContext dbcontext)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            await _next(context);
            return;
        }

        var claimsIdentity = context.User.Identity as ClaimsIdentity;

        if (claimsIdentity == null)
        {
            await _next(context);
            return;
        }

        var jwthelper = new JwtHelper(claimsIdentity);
        var userId = jwthelper.GetClaimValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            await _next(context);
            return;
        }

        var user = await new User.Select(dbcontext).FromId(Guid.Parse(userId));

        if (user == null)
        {
            await _next(context);
            return;
        }

        var claims = new[]
        {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ?? Guid.Empty.ToString()),
        new Claim(ClaimTypes.Name, user.UserName ?? ""),
        new Claim(ClaimTypes.Email, user.Email ?? ""),
      };

        var identity = new ClaimsIdentity(claims, "custom");
        var principal = new ClaimsPrincipal(identity);

        context.User = principal;
        await _next(context);
    }
}

public static class AuthMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthMiddleWare(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}
