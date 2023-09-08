using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PokeJournal.Models;

using PokeJournal.Data;

using Pokemon = PokeJournal.Usecases.Pokemon;
using User = PokeJournal.Usecases.User;
using PokeJournal.Helpers;
using PokeJournal.Exceptions;

namespace PokeJournal.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PokemonController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PokemonController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("All/{userId:Guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<List<FavoritePokemonModel>>> AllFromUser(Guid userId)
    {
        var r = await new Pokemon.Select(_context).AllFromUser(userId);
        return r;
    }

    [HttpPost("Favorite/{pokemonIndex}")]
    public async Task<ActionResult<FavoritePokemonModel>> Favorite(int pokemonIndex)
    {
        var userId = AuthHelper.UserId(User);

        var user = await new User.Select(_context).FromId(userId) ?? UserNotFounded();

        var r = await new Pokemon.Favorite(_context, user, pokemonIndex).Execute();
        r.User = null;
        return r;
    }

    [HttpPost("Unfavorite/{pokemonIndex}")]
    public async Task<ActionResult<FavoritePokemonModel>> Unfavorite(int pokemonIndex)
    {
        var userId = AuthHelper.UserId(User);

        var user = await new User.Select(_context).FromId(userId) ?? UserNotFounded();

        await new Pokemon.Unfavorite(_context, user, pokemonIndex).Execute();
        return Ok("Pokemon unfavorited");
    }

    private UserModel UserNotFounded(){
        throw new NotFoundException("User not founded.");
    }
}
