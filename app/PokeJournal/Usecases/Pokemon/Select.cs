using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.Pokemon;

public class Select{
    private readonly ApplicationDbContext _context;

    public Select(ApplicationDbContext context){
      _context = context;
    }

    public async Task<List<FavoritePokemonModel>> AllFromUser(Guid userId){
      var pokemons = await _context.FavoritePokemons
        .Where(pokemon => pokemon.UserId == userId)
        .Select(x => new FavoritePokemonModel {
          Id = x.Id,
          PokemonIndex = x.PokemonIndex,
          UserId = x.UserId,
          CreatedAt = x.CreatedAt,
          UpdatedAt = x.UpdatedAt
          })
        .ToListAsync();
      return pokemons;
    }
}
