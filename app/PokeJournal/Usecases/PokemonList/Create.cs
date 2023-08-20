using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.Providers.PokeAPI; 

namespace PokeJournal.Usecases.PokemonList;

public class Create{
    private readonly ApplicationDbContext _context;
    private readonly PokemonListModel pokemon;
    private readonly IPokeAPIProvider _pokeApi;

    public Create(ApplicationDbContext context, int pokemonIndex, string customName){
      _context = context;

      _pokeApi = new PokeAPI();

      this.pokemon = new PokemonListModel {
        PokemonIndex = pokemonIndex,
        CustomName = customName
      };

    }

    public PokemonListModel Execute(){
      var pokemonInfos = _pokeApi.GetBasicInfos(this.pokemon.PokemonIndex.ToString()).Result;

      if (pokemonInfos == null || pokemonInfos.name == null){
        throw new Exception($"Pokemon with index: '{this.pokemon.PokemonIndex}', Not Founded");
      }

      this.pokemon.DefaultName = pokemonInfos.name;
      var savedPokemon = _context.PokemonLists.Add(this.pokemon);
      _context.SaveChanges();
      return savedPokemon.Entity;
    }
}
