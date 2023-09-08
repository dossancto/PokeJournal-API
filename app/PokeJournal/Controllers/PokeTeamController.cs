using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using PokeJournal.Models;
using PokeJournal.DTO;

using PokeJournal.Data;

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

    public PokeTeamController(ApplicationDbContext context)
    {
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet("List/{userId:Guid}")]
    public async Task<ActionResult<List<PokeTeamModel>>> ListTeams(Guid userId)
    {
        var teams = (await new PokeTeam.Select(_context).AllFromUser(userId))
          .Select(team =>
          {
              team.Pokemons = team.Pokemons
            .Select(p =>
            {
                p.PokeTeam = null;
                return p;
            }).ToList();
              return team;
          }).ToList();

        return teams;
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<ActionResult<PokeTeamModel>> ShowTeam(Guid id)
    {
        var team = await new PokeTeam.Select(_context).FromId(id);

        team.Pokemons = team.Pokemons
          .Select(p =>
          {
              p.PokeTeam = null;
              return p;
          }).ToList();

        return team;
    }

    [HttpPost]
    [Route("New")]
    public async Task<ActionResult<PokeTeamModel>> CreateTeam(PokeTeamDTO teamDTO)
    {
        var token = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = token != null ? Guid.Parse(token) : Guid.Empty;

        if (userId == Guid.Empty)
        {
            return NotFound("User not founded.");
        }

        var team = await (await new PokeTeam.Create(_context, teamDTO.pokemonIndex, teamDTO.name, teamDTO.description).FromUserId(userId)).Execute();
        team.User = null;
        team.Pokemons = team.Pokemons
        .Select(p =>
        {
            p.PokeTeam = null;
            return p;
        }).ToList();

        return CreatedAtAction(
            nameof(ShowTeam),
            new { id = team.Id },
            team);
    }

    [HttpDelete("Delete/{teamId:Guid}")]
    public async Task<ActionResult<PokeTeamModel>> CreateTeam(Guid teamId)
    {
        var token = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = token != null ? Guid.Parse(token) : Guid.Empty;

        if (userId == Guid.Empty)
        {
            return NotFound("User not founded.");
        }

        var team = await new PokeTeam.Select(_context).FromId(teamId);

        if (team.UserId != userId)
        {
            return StatusCode(403, "You don't have access to this team.");
        }

        await new PokeTeam.Delete(_context, teamId).Execute();

        return Ok("Team deleted");
    }

    [HttpPost]
    [Route("AddPokemon")]
    public async Task<ActionResult<PokemonListDTO>> AddPokemonToTeam(AddPokemonDTO addpokemonDTO)
    {
        var token = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = token != null ? Guid.Parse(token) : Guid.Empty;

        if (userId == Guid.Empty)
        {
            return NotFound("User not founded.");
        }

        var team = await new PokeTeam.Select(_context).FromId(addpokemonDTO.teamId);

        if (team.UserId != userId)
        {
            return StatusCode(403, "You don't have access to this team.");
        }

        var pokemon = await new PokeTeam.AddPokemon(_context, addpokemonDTO.pokemonIndex, addpokemonDTO.customName, team).Execute();

        return new PokemonListDTO(pokemon.DefaultName, pokemon.CustomName, pokemon.ImgURL, pokemon.PokeTeamId, pokemon.PokemonIndex);
    }

    [HttpDelete("RemovePokemon/{teamId:Guid}/{pokemonId:Guid}")]
    public async Task<ActionResult<PokemonListDTO>> RemovePokemonOfTeam(Guid teamId, Guid pokemonId)
    {
        var token = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = token != null ? Guid.Parse(token) : Guid.Empty;

        if (userId == Guid.Empty)
        {
            return NotFound("User not founded.");
        }

        var team = await new PokeTeam.Select(_context).FromId(teamId);

        if (team == null)
        {
            return NotFound($"Team with id: '{teamId}' not founded.");
        }

        if (team.UserId != userId)
        {
            return StatusCode(403, "You don't have access to this team.");
        }

        await new PokeTeam.RemovePokemon(_context, pokemonId).Execute();

        return Ok("Pokemon Removed");
    }

    [AllowAnonymous]
    [HttpPut("/ChangePokemonName/{pokeId:Guid}")]
    public async Task<ActionResult<PokemonListModel>> ChangePokemonName(Guid pokeId, string? newName)
    {
        return await new PokeTeam.ChangePokeName(_context, pokeId, newName ?? "").Execute();
    }
}
