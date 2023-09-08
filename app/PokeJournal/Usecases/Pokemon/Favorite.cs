using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.Pokemon;

public class Favorite
{
    private readonly ApplicationDbContext _context;

    private readonly FavoritePokemonModel favorite;

    public Favorite(ApplicationDbContext context, UserModel user, int pokemonIndex)
    {
        _context = context;

        this.favorite = new FavoritePokemonModel
        {
            PokemonIndex = pokemonIndex,
            User = user
        };
    }

    public async Task<FavoritePokemonModel> Execute()
    {
        var isFavorite = await _context.FavoritePokemons
          .FirstOrDefaultAsync(p => p.PokemonIndex == this.favorite.PokemonIndex && p.UserId == this.favorite.User.Id);

        if (isFavorite != null)
        {
            return isFavorite;
        }

        var saved = _context.FavoritePokemons.Add(this.favorite);
        await _context.SaveChangesAsync();
        return saved.Entity;
    }
}
