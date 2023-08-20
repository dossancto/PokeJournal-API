namespace PokeJournal.Test.Usecases.User;

using User = PokeJournal.Usecases.User;
using PokeJournal.Data;
using PokeJournal.Models;

using Microsoft.EntityFrameworkCore;
using Xunit;

public class RegisterUserTest: IDisposable
{
  private readonly ApplicationDbContext _context;

    public RegisterUserTest(){
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
    public void SuccessfullRegisterUser()
    {
      var user = new User.Register(_context, "test user", "test@email.com", "test123").Execute();

      Assert.NotEqual("test123", user.Password);

      Assert.Equal("test@email.com", user.Email);

      Assert.NotEqual(Guid.Empty, user.Id);
    }

    [Fact]
    public void FailOnEmptyPassword(){
      var c = new User.Register(_context, "test user", "test@email.com", "");

      Assert.Throws<Exception>(() => c.Execute());
    }

    [Fact]
    public void FailOnEmptyInfos(){
      var u = new User.Register(_context, "", null, "a");

      Assert.Throws<DbUpdateException>(() => u.Execute());
    }
}
