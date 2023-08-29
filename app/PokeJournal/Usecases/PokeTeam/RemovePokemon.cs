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

    public void Execute(){
      var pokemonToRemove = _context.PokemonLists.Find(this.pokemonId);

      if(pokemonToRemove == null){
        return;
      }

      _context.PokemonLists.Remove(pokemonToRemove);
      _context.SaveChanges();
    }
}
