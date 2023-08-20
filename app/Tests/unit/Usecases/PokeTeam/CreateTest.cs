namespace PokeJournal.Test.Usecases.PokeTeam;

using PokeTeam = PokeJournal.Usecases.PokeTeam;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class CreateTest: IDisposable
{
    private readonly ApplicationDbContext _context;

    public CreateTest(){
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "Poke Journal")
        .Options;

        _context = new ApplicationDbContext(options);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public void SuccessfullCreateNewTeam()
    {
      var inserted = new PokeTeam.Create(_context, 1, "My First Team", "Some description").Execute();

      Assert.NotEqual(Guid.Empty, inserted.Id);
      Assert.Equal("My First Team", inserted.Name);
      Assert.Single(inserted.Pokemons);
    }
}
