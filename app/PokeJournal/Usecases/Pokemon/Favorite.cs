using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.Pokemon;

public class Favorite{
    private readonly ApplicationDbContext _context;

    private readonly FavoritePokemonModel favorite;

    public Favorite(ApplicationDbContext context, UserModel user, int pokemonIndex){
      _context = context;

      this.favorite = new FavoritePokemonModel {
        PokemonIndex = pokemonIndex,
        User = user
      };
    }

    public FavoritePokemonModel Execute(){
      var isFavorite = _context.FavoritePokemons.FirstOrDefault(p => p.PokemonIndex == this.favorite.PokemonIndex && p.UserId == this.favorite.User.Id);

      if(isFavorite != null){
        return isFavorite;
      }

      var saved = _context.FavoritePokemons.Add(this.favorite);
      _context.SaveChanges();
      return saved.Entity;
    }
}
