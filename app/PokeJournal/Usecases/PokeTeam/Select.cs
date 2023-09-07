using System.Linq;
using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.Providers.PokeAPI; 

namespace PokeJournal.Usecases.PokeTeam;

public class Select{
    private readonly ApplicationDbContext _context;

    public Select(ApplicationDbContext context){
      _context = context;
    }

    public async Task<PokeTeamModel> FromId(Guid teamId){
      var team = await _context.PokeTeams
        .Include(t => t.Pokemons)
        .FirstOrDefaultAsync(t => t.Id == teamId);

      return team;
    }

    public async Task<List<PokeTeamModel>> AllFromUser(Guid userId){
     var teams = await _context.PokeTeams
       .Where(team => team.UserId == userId)
       .Include(t => t.Pokemons)
       .ToListAsync();

      return teams;
    }
}
