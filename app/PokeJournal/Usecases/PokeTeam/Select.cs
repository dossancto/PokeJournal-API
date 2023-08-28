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
      var team = _context.PokeTeams.Find(teamId);

      team.Pokemons = _context.PokemonLists
        .Where(x => x.PokeTeamId == teamId)
        .Select(x => new PokemonListModel{
            Id = x.Id,
            PokeTeamId= x.PokeTeamId,
            DefaultName = x.DefaultName,
            CustomName = x.CustomName,
            PokemonIndex = x.PokemonIndex,
            ImgURL = x.ImgURL
            })
        .ToList();

      Console.WriteLine(team.Id);
      Console.WriteLine(team.Pokemons);
      return team;
    }
}
