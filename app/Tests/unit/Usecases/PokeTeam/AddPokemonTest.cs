namespace PokeJournal.Test.Usecases.PokeTeam;

using PokeTeam = PokeJournal.Usecases.PokeTeam;
using User = PokeJournal.Usecases.User;

using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class AddPokemonTest : IDisposable
{
    private readonly ApplicationDbContext _context;
    private readonly PokeTeam.Create _baseTeam;

    public AddPokemonTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "Organizart")
        .Options;

        _context = new ApplicationDbContext(options);

        var createUser = new User.Register(_context, "test user", "test@email.com", "test123").Execute();
        createUser.Wait();
        var user = createUser.Result;
        _baseTeam = new PokeTeam.Create(_context, 1, "My First Team", "Some description").FromUser(user);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task SuccessfullCreateNewPokemon()
    {
        var team = await _baseTeam.Execute();
        var insertedPokemon = await new PokeTeam.AddPokemon(_context, 4, "Vermelin", team).Execute();

        Assert.NotEqual(Guid.Empty, insertedPokemon.Id);
        Assert.Equal("Vermelin", insertedPokemon.CustomName);
        Assert.Equal("charmander", insertedPokemon.DefaultName.ToLower());
        Assert.Equal(2, team.Pokemons.Count);
    }

    [Fact]
    public async Task FailOnNotFoundedPokemon()
    {
        var team = await _baseTeam.Execute();
        var insertedPokemon = new PokeTeam.AddPokemon(_context, 99999999, "Nobody", team);

        var exception = await Assert.ThrowsAsync<Exception>(async () => await insertedPokemon.Execute());
        Assert.Equal("Pokemon with index: '99999999', Not Founded", exception.Message);
        Assert.Single(team.Pokemons);
    }

    [Fact]
    public async Task FailOn_MaxPokemon()
    {
        var team = await _baseTeam.Execute();

        await new PokeTeam.AddPokemon(_context, 1, "one", team).Execute();
        await new PokeTeam.AddPokemon(_context, 2, "two", team).Execute();
        await new PokeTeam.AddPokemon(_context, 3, "tree", team).Execute();
        await new PokeTeam.AddPokemon(_context, 4, "four", team).Execute();

        var insertPokemon = new PokeTeam.AddPokemon(_context, 4, "Premiado", team);

        var exception = await Assert.ThrowsAsync<Exception>(async () => await insertPokemon.Execute());
        Assert.Equal("You can have only five pokemons on a team.", exception.Message);

        Assert.Equal(5, team.Pokemons.Count);
    }
}
