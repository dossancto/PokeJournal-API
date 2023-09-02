namespace PokeJournal.Test.Usecases.PokeTeam;

using PokeTeam = PokeJournal.Usecases.PokeTeam;
using User = PokeJournal.Usecases.User;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class CreateTest: IDisposable
{
    private readonly ApplicationDbContext _context;

    private readonly UserModel _user;

    public CreateTest(){
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "Poke Journal")
        .Options;

        _context = new ApplicationDbContext(options);
        var createUser = new User.Register(_context, "test user", "test@email.com", "test123").Execute();
        createUser.Wait();
        _user = createUser.Result;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task SuccessfullCreateNewTeam()
    {
      var inserted = await new PokeTeam.Create(_context, _user, 1, "My First Team", "Some description").Execute();

      Assert.NotEqual(Guid.Empty, inserted.Id);
      Assert.Equal("My First Team", inserted.Name);
      Assert.Single(inserted.Pokemons);
    }
}
