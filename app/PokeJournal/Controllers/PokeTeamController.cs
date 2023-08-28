using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PokeJournal.Models;
using PokeJournal.DTO;

using PokeJournal.Data;
using PokeJournal.Helpers;

// Use cases
using PokeTeam = PokeJournal.Usecases.PokeTeam;
using User = PokeJournal.Usecases.User;

namespace PokeJournal.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class PokeTeamController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public PokeTeamController(ApplicationDbContext context){
    _context = context;
  }

  [HttpGet("{id}")]
  [AllowAnonymous]
  public async Task<ActionResult<PokeTeamModel>> ShowTeam(Guid id)
  {
    var team = new PokeTeam.Select(_context).FromId(id);

    return team;
  }

  [HttpPost]
  [Route("New")]
  public async Task<ActionResult<PokeTeamModel>> CreateTeam(PokeTeamDTO teamDTO)
  {
      var claimsIdentity = User.Identity as ClaimsIdentity;
      var jwthelper = new JwtHelper(claimsIdentity);

      var userId = jwthelper.GetClaimValue(ClaimTypes.NameIdentifier);

      var user = new User.Select(_context).FromId(Guid.Parse(userId));
      var team = new PokeTeam.Create(_context, user, teamDTO.pokemonIndex, teamDTO.name, teamDTO.description).Execute();

      return CreatedAtAction(
          nameof(ShowTeam),
          new { id = team.Id },
          team);
  }

  [HttpPost]
  [Route("AddPokemon")]
  public async Task<ActionResult<PokemonListDTO>> AddPokemonToTeam(AddPokemonDTO addpokemonDTO)
  {
      var claimsIdentity = User.Identity as ClaimsIdentity;
      var jwthelper = new JwtHelper(claimsIdentity);
      var userId = Guid.Parse(jwthelper.GetClaimValue(ClaimTypes.NameIdentifier));

      var team = new PokeTeam.Select(_context).FromId(addpokemonDTO.teamId);

      if(team.UserId != userId){
        return StatusCode(403, "You don't have access to this team.");
      }

      var pokemon = new PokeTeam.AddPokemon(_context, addpokemonDTO.pokemonIndex, addpokemonDTO.customName, team).Execute();

      return new PokemonListDTO(pokemon.DefaultName, pokemon.CustomName, pokemon.ImgURL, pokemon.PokeTeamId, pokemon.PokemonIndex);
  }
}
