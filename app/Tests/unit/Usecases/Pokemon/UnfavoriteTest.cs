namespace PokeJournal.Test.Usecases.Pokemon;

using Pokemon = PokeJournal.Usecases.Pokemon;
using User = PokeJournal.Usecases.User;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class UnfavoriteTest : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly User.Register _baseUser;

    public UnfavoriteTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "Poke Journal")
        .Options;

        _context = new ApplicationDbContext(options);

        _baseUser = new User.Register(_context, "test user", "test@email.com", "test123");
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task Successfull_UnfavoritePokemon()
    {
        var user = await _baseUser.Execute();
        new Pokemon.Unfavorite(_context, user, 1).Execute();
    }
}
