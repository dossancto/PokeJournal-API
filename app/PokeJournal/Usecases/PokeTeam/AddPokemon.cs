using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.Providers.PokeAPI; 

namespace PokeJournal.Usecases.PokeTeam;

public class AddPokemon{
    private readonly ApplicationDbContext _context;

    private readonly PokemonListModel pokemon;
    private readonly PokeTeamModel team;

    private readonly IPokeAPIProvider _pokeApi;

    public AddPokemon(ApplicationDbContext context, int pokemonIndex, string customName, PokeTeamModel team){
      _context = context;

      _pokeApi = new PokeAPI();

      this.team = team;
      this.pokemon = new PokemonListModel {
        PokemonIndex = pokemonIndex,
        CustomName = customName
      };

    }

    public PokemonListModel Execute(){
      this.pokemon.PokeTeam = this.team;

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
