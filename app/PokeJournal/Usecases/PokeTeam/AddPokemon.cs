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
      Valid();

      this.pokemon.PokeTeam = this.team;

      var pokemonInfos = GetPokemonInfos();

      this.pokemon.DefaultName = pokemonInfos.name;

      return Save();
    }

    private void Valid(){
      if (this.team.Pokemons == null) {
        return;
      }

      if(this.team.Pokemons.Count >= 5){
        throw new Exception("You can have only five pokemons on a team.");
      }
    }

    private PokemonResponse GetPokemonInfos(){
      var pokemonInfos = _pokeApi.GetBasicInfos(this.pokemon.PokemonIndex.ToString()).Result;

      if (pokemonInfos == null || pokemonInfos.name == null){
        throw new Exception($"Pokemon with index: '{this.pokemon.PokemonIndex}', Not Founded");
      }

      return pokemonInfos;
    }

    private PokemonListModel Save(){
      var savedPokemon = _context.PokemonLists.Add(this.pokemon);
      _context.SaveChanges();
      return savedPokemon.Entity;
    }
}
