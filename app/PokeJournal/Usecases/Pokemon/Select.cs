using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.Pokemon;

public class Select{
    private readonly ApplicationDbContext _context;

    public Select(ApplicationDbContext context){
      _context = context;
    }

    public List<FavoritePokemonModel> AllFromUser(Guid userId){
      var pokemons = _context.FavoritePokemons
        .Where(pokemon => pokemon.UserId == userId)
        .Select(x => new FavoritePokemonModel {
          Id = x.Id,
          PokemonIndex = x.PokemonIndex,
          UserId = x.UserId,
          CreatedAt = x.CreatedAt,
          UpdatedAt = x.UpdatedAt
          })
        .ToList();
      return pokemons;
    }
}
