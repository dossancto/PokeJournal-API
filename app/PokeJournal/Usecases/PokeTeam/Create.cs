using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.PokeTeam;

public class Create{
    private readonly ApplicationDbContext _context;
    private readonly PokeTeamModel pokemonTeam;
    private readonly int pokemonIndex;

    public Create(ApplicationDbContext context, int pokemonIndex, string name, string description = ""){
      _context = context;

      this.pokemonIndex = pokemonIndex;
      this.pokemonTeam = new PokeTeamModel {
        Name = name,
        Description = description
      };
    }

    public PokeTeamModel Execute(){
      var saved = _context.PokeTeams.Add(this.pokemonTeam);
      _context.SaveChanges();
      var team = saved.Entity;

      new PokeTeam.AddPokemon(_context, 1, "Verdin", team).Execute();

      return team;
    }
}
