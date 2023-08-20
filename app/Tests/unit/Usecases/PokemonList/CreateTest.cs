namespace PokeJournal.Test.Usecases.PokemonList;

using PokemonList = PokeJournal.Usecases.PokemonList;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class CreateTest: IDisposable
{
    private readonly ApplicationDbContext _context;

    public CreateTest(){
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "Organizart")
        .Options;

        _context = new ApplicationDbContext(options);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void SuccessfullCreateNewPokemon()
    {
      var insertedPokemon = new PokemonList.Create(_context, 1, "Verdin").Execute();

      Assert.NotEqual(Guid.Empty, insertedPokemon.Id);
      Assert.Equal("Verdin", insertedPokemon.CustomName);
      Assert.Equal("bulbasaur", insertedPokemon.DefaultName.ToLower());
    }

    [Fact]
    public void FailOnNotFoundedPokemon()
    {
      var insertedPokemon = new PokemonList.Create(_context, 99999999, "Nobody");

      var exception = Assert.Throws<Exception>(() => insertedPokemon.Execute());
      Assert.Equal("Pokemon with index: '99999999', Not Founded", exception.Message);
    }
}
