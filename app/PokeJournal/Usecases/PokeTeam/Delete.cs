using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.PokeTeam;

public class Delete{
    private readonly ApplicationDbContext _context;
    private readonly Guid teamId;

    public Delete(ApplicationDbContext context, Guid id){
      _context = context;

      this.teamId = id;
    }

    public void Execute(){
      var teamToRemove = _context.PokeTeams.FirstOrDefault(p => p.Id == this.teamId);

      if(teamToRemove == null){
        return;
      }

      _context.PokeTeams.Remove(teamToRemove);
      _context.SaveChanges();
    }
}
