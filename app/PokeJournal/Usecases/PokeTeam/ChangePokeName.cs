using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.PokeTeam;

public class ChangePokeName
{
    private readonly ApplicationDbContext _context;
    private readonly PokemonListModel? pokemon;
    private readonly string newName;

    public ChangePokeName(ApplicationDbContext context, Guid pokeId, string newName)
    {
        _context = context;
        this.pokemon = LoadFromId(pokeId);
        this.newName = newName;
    }
    public ChangePokeName(ApplicationDbContext context, PokemonListModel pokemon, string newName)
    {
        _context = context;
        this.pokemon = pokemon;
        this.newName = newName;
    }


    public async Task<PokemonListModel> Execute()
    {
        if (this.pokemon == null)
        {
            throw new Exception("Pokemon not founded");
        }

        this.pokemon.CustomName = this.newName;

        await _context.SaveChangesAsync();
        return this.pokemon;
    }

    private PokemonListModel? LoadFromId(Guid id)
    {
        return _context.PokemonLists.Find(id);
    }
}
