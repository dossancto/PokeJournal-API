using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.Pokemon;

public class Unfavorite{
    private readonly ApplicationDbContext _context;

    private readonly int pokemonIndex;

    public Unfavorite(ApplicationDbContext context, UserModel user, int pokemonIndex){
      _context = context;

      this.pokemonIndex = pokemonIndex;
    }

    public void Execute(){
      var pokemonToRemove = _context.FavoritePokemons.FirstOrDefault(p => p.PokemonIndex == this.pokemonIndex);

      if(pokemonToRemove == null){
        return;
      }

      _context.FavoritePokemons.Remove(pokemonToRemove);
      _context.SaveChanges();
    }
}
