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

        var user = new User.Register(_context, "test user", "test@email.com", "test123").Execute();
        _baseTeam = new PokeTeam.Create(_context, user, 1, "My First Team", "Some description");
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
