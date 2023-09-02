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
      var team = await _context.PokeTeams.FindAsync(teamId);

      team.Pokemons = await _context.PokemonLists
        .Where(x => x.PokeTeamId == teamId)
        .Select(x => new PokemonListModel{
            Id = x.Id,
            PokeTeamId= x.PokeTeamId,
            DefaultName = x.DefaultName,
            CustomName = x.CustomName,
            PokemonIndex = x.PokemonIndex,
            ImgURL = x.ImgURL
            })
        .ToListAsync();

      return team;
    }

    public async Task<List<PokeTeamModel>> AllFromUser(Guid userId){
     var teams = await _context.PokeTeams.Where(team => team.UserId == userId).ToListAsync();
      return teams;
    }
}
