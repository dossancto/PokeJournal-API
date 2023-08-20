namespace PokeJournal.Test.Usecases.PokeTeam;

using PokeTeam = PokeJournal.Usecases.PokeTeam;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class AddPokemonTest: IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly PokeTeam.Create _baseTeam;

    public AddPokemonTest(){
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
    public void SuccessfullCreateNewPokemon()
    {
      var team = _baseTeam.Execute();
      var insertedPokemon = new PokeTeam.AddPokemon(_context, 4, "Vermelin", team).Execute();

      Assert.NotEqual(Guid.Empty, insertedPokemon.Id);
      Assert.Equal("Vermelin", insertedPokemon.CustomName);
      Assert.Equal("charmander", insertedPokemon.DefaultName.ToLower());
      Assert.Equal(2, team.Pokemons.Count);
    }

    [Fact]
    public void FailOnNotFoundedPokemon()
    {
      var team = _baseTeam.Execute();
      var insertedPokemon = new PokeTeam.AddPokemon(_context, 99999999, "Nobody", team);

      var exception = Assert.Throws<Exception>(() => insertedPokemon.Execute());
      Assert.Equal("Pokemon with index: '99999999', Not Founded", exception.Message);
      Assert.Single(team.Pokemons);
    }
}
