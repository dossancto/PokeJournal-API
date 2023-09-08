using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.Providers.PokeAPI;

namespace PokeJournal.Usecases.PokeTeam;

public class AddPokemon
{
    private readonly ApplicationDbContext _context;

    private readonly PokemonListModel pokemon;
    private readonly PokeTeamModel team;

    private readonly IPokeAPIProvider _pokeApi;

    public AddPokemon(ApplicationDbContext context, int pokemonIndex, string customName, PokeTeamModel team)
    {
        _context = context;

        _pokeApi = new PokeAPI();

        this.team = team;
        this.pokemon = new PokemonListModel
        {
            PokemonIndex = pokemonIndex,
            CustomName = customName
        };

    }

    public async Task<PokemonListModel> Execute()
    {
        Valid();

        this.pokemon.PokeTeam = this.team;

        var pokemonInfos = await GetPokemonInfos();

        this.pokemon.DefaultName = pokemonInfos.name;

        return await Save();
    }

    private void Valid()
    {
        if (this.team.Pokemons == null)
        {
            return;
        }

        if (this.team.Pokemons.Count >= 5)
        {
            throw new Exception("You can have only five pokemons on a team.");
        }
    }

    private async Task<PokemonResponse> GetPokemonInfos()
    {
        var pokemonInfos = await this._pokeApi
          .GetBasicInfos(this.pokemon.PokemonIndex.ToString());

        if (pokemonInfos == null || pokemonInfos.name == null)
        {
            throw new Exception($"Pokemon with index: '{this.pokemon.PokemonIndex}', Not Founded");
        }

        return pokemonInfos;
    }

    private async Task<PokemonListModel> Save()
    {
        var savedPokemon = _context.PokemonLists.Add(this.pokemon);
        await _context.SaveChangesAsync();
        return savedPokemon.Entity;
    }
}
