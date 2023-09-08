using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.Pokemon;

public class Unfavorite
{
    private readonly ApplicationDbContext _context;

    private readonly int pokemonIndex;

    public Unfavorite(ApplicationDbContext context, UserModel user, int pokemonIndex)
    {
        _context = context;

        this.pokemonIndex = pokemonIndex;
    }

    public async Task Execute()
    {
        var pokemonToRemove = await _context.FavoritePokemons.FirstOrDefaultAsync(p => p.PokemonIndex == this.pokemonIndex);

        if (pokemonToRemove == null)
        {
            return;
        }

        _context.FavoritePokemons.Remove(pokemonToRemove);
        await _context.SaveChangesAsync();
    }
}
