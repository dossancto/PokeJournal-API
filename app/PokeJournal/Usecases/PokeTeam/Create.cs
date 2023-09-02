using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.DTO;

namespace PokeJournal.Usecases.PokeTeam;

public class Create{
    private readonly ApplicationDbContext _context;
    private readonly PokeTeamModel pokemonTeam;
    private readonly int pokemonIndex;

    public Create(ApplicationDbContext context, UserModel user, PokeTeamDTO dto): this(context, user, dto.pokemonIndex, dto.name, dto.description) {}

    public Create(ApplicationDbContext context, UserModel user, int pokemonIndex, string name, string description = ""){
      _context = context;

      this.pokemonIndex = pokemonIndex;
      this.pokemonTeam = new PokeTeamModel {
        Name = name,
        Description = description,
        User = user
      };
    }

    public async Task<PokeTeamModel> Execute(){
      var saved = _context.PokeTeams.Add(this.pokemonTeam);
      await _context.SaveChangesAsync();
      var team = saved.Entity;

      new PokeTeam.AddPokemon(_context, 1, "Verdin", team).Execute();

      return team;
    }
}
