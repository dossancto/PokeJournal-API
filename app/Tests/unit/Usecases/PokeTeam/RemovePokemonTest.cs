namespace PokeJournal.Test.Usecases.PokeTeam;

using PokeTeam = PokeJournal.Usecases.PokeTeam;
using User = PokeJournal.Usecases.User;

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
    public async Task Successfull_RemoveAPokemon()
    {
      var team = await _baseTeam.Execute();
      var insertedPokemon = await new PokeTeam.AddPokemon(_context, 4, "Vermelin", team).Execute();

      Assert.Equal(2, team.Pokemons.Count);

      await new PokeTeam.RemovePokemon(_context, insertedPokemon.Id).Execute();

      Assert.Single(team.Pokemons);
    }

    [Fact]
    public async Task Successfull_RemoveAllPokemons()
    {
      var team = await _baseTeam.Execute();
      var insertedPokemon = await new PokeTeam.AddPokemon(_context, 4, "Vermelin", team).Execute();

      Assert.Equal(2, team.Pokemons.Count);

      PokemonListModel firstPokemon = team.Pokemons.FirstOrDefault();

      await new PokeTeam.RemovePokemon(_context, insertedPokemon.Id).Execute();
      await new PokeTeam.RemovePokemon(_context, firstPokemon.Id).Execute();

      Assert.Empty(team.Pokemons);
    }
}
