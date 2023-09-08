namespace PokeJournal.Test.Usecases.Pokemon;

using Pokemon = PokeJournal.Usecases.Pokemon;
using User = PokeJournal.Usecases.User;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class FavoriteTest : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly User.Register _baseUser;

    public FavoriteTest()
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
    public async Task Successfull_FavoritePokemon()
    {
        var user = await _baseUser.Execute();
        var inserted = await new Pokemon.Favorite(_context, user, 1).Execute();

        Assert.NotEqual(Guid.Empty, inserted.Id);
        Assert.Equal(user.Id, inserted.User.Id);
        Assert.Single(user.FavoritePokemons);
    }

    [Fact]
    public async Task Success_SamePokemon()
    {
        var user = await _baseUser.Execute();
        await new Pokemon.Favorite(_context, user, 1).Execute();
        var favorite = await new Pokemon.Favorite(_context, user, 1).Execute();

        Assert.NotEqual(Guid.Empty, favorite.Id);
        Assert.Single(user.FavoritePokemons);
    }
}
