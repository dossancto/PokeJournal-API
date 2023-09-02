using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.Providers.PokeAPI; 

namespace PokeJournal.Usecases.PokeTeam;

public class RemovePokemon{
    private readonly ApplicationDbContext _context;

    private readonly Guid pokemonId;

    public RemovePokemon(ApplicationDbContext context, Guid id){
      _context = context;

      this.pokemonId = id;
    }

    public async Task Execute(){
      var pokemonToRemove = await _context.PokemonLists.FindAsync(this.pokemonId);

      if(pokemonToRemove == null){
        return;
      }

      _context.PokemonLists.Remove(pokemonToRemove);
      await _context.SaveChangesAsync();
    }
}
