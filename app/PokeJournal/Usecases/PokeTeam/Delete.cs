using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;

namespace PokeJournal.Usecases.PokeTeam;

public class Delete
{
    private readonly ApplicationDbContext _context;
    private readonly Guid teamId;

    public Delete(ApplicationDbContext context, Guid id)
    {
        _context = context;

        this.teamId = id;
    }

    public async Task Execute()
    {
        var teamToRemove = await _context.PokeTeams.FirstOrDefaultAsync(p => p.Id == this.teamId);

        if (teamToRemove == null)
        {
            return;
        }

        _context.PokeTeams.Remove(teamToRemove);
        await _context.SaveChangesAsync();
    }
}
