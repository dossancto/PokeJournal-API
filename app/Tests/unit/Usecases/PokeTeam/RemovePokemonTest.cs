namespace PokeJournal.Test.Usecases.PokeTeam;

using PokeTeam = PokeJournal.Usecases.PokeTeam;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class RemovePokemonTest: IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly PokeTeam.Create _baseTeam;

    public RemovePokemonTest(){
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "Organizart")
        .Options;

        _context = new ApplicationDbContext(options);

        _baseTeam = new PokeTeam.Create(_context, 1, "My First Team", "Some description");
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void Successfull_RemoveAPokemon()
    {
      var team = _baseTeam.Execute();
      var insertedPokemon = new PokeTeam.AddPokemon(_context, 4, "Vermelin", team).Execute();

      Assert.Equal(2, team.Pokemons.Count);

      new PokeTeam.RemovePokemon(_context, insertedPokemon.Id).Execute();

      Assert.Single(team.Pokemons);
    }

    [Fact]
    public void Successfull_RemoveAllPokemons()
    {
      var team = _baseTeam.Execute();
      var insertedPokemon = new PokeTeam.AddPokemon(_context, 4, "Vermelin", team).Execute();

      Assert.Equal(2, team.Pokemons.Count);

      PokemonListModel firstPokemon = team.Pokemons.FirstOrDefault();

      new PokeTeam.RemovePokemon(_context, insertedPokemon.Id).Execute();
      new PokeTeam.RemovePokemon(_context, firstPokemon.Id).Execute();

      Assert.Empty(team.Pokemons);
    }
}
