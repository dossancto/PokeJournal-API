// namespace PokeJournal.Test.Usecases.PokeTeam;

// using PokeTeam = PokeJournal.Usecases.PokeTeam;

// using PokeJournal.Data;
// using PokeJournal.Models;

// using Microsoft.EntityFrameworkCore;
// using Xunit;

// public class DeleteTest: IDisposable
// {
//     private readonly ApplicationDbContext _context;

//     public DeleteTest(){
//         var options = new DbContextOptionsBuilder<ApplicationDbContext>()
//         .UseInMemoryDatabase(databaseName: "Poke Journal")
//         .Options;

//         _context = new ApplicationDbContext(options);
//     }

//     public void Dispose()
//     {
//         _context.Dispose();
//     }

//     [Fact]
//     public void SuccessfullDeleteATeam()
//     {
//       Console.WriteLine("Delete team");
//       var inserted = new PokeTeam.Create(_context, 1, "My First Team", "Some description").Execute();
//       new PokeTeam.Delete(_context, inserted.Id).Execute();

//       Assert.Equal(Guid.Empty, inserted.Id);
//     }

//     [Fact]
//     public void SuccessfullDeleteATeamWithManyPokemons()
//     {
//       Console.WriteLine("Delete team 2");
//       var inserted = new PokeTeam.Create(_context, 1, "My First Team", "Some description").Execute();
//       new PokeTeam.AddPokemon(_context, 1, "one", inserted).Execute();

//       new PokeTeam.Delete(_context, inserted.Id).Execute();

//       Assert.Equal(Guid.Empty, inserted.Id);
//     }
// }
