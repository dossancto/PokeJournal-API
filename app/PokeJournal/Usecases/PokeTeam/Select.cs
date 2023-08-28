using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.Providers.PokeAPI; 

namespace PokeJournal.Usecases.PokeTeam;

public class Select{
    private readonly ApplicationDbContext _context;

    public Select(ApplicationDbContext context){
      _context = context;
    }

    public PokeTeamModel FromId(Guid teamId){
      var team = _context.PokeTeams.FirstOrDefault(team => team.Id == teamId);
      return team;
    }
}
