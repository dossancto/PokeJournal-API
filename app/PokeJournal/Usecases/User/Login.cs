using System.Text;

using Microsoft.EntityFrameworkCore;

using PokeJournal.Models;
using PokeJournal.Data;
using PokeJournal.DTO;
using PokeJournal.Providers.Criptografy;

namespace PokeJournal.Usecases.User;

public class Login{
    private readonly ApplicationDbContext _context;
    private readonly ICriptografyProvider crypto;
    private readonly UserModel userdto;

    public Login(ApplicationDbContext context, string email, string password){
      _context = context;

      this.userdto = new UserModel {
        UserName = "",
        Email = email,
        Password = password
      };

      crypto = new Pbkdf2();
    }

    public async Task<UserModel> Execute(){
      string? email = this.userdto.Email;
      string? password = this.userdto.Password;

      if(email == null || password == null){
        throw new Exception("Pleace inform an Email and Password");
      }

      var storedUser = await _context.Users.FirstOrDefaultAsync(user => user.Email == email);

      if(storedUser == null){
        throw new Exception($"User with email '{email}' not founded. Consider Create a new Account.");
      }

      bool isCredentialsValid = crypto.VerifyPassword(password, storedUser.Password ?? "", storedUser.Salt ?? "");

      if (!isCredentialsValid){
        throw new Exception("Email or Password wrong");
      }

      return storedUser;
    }
}
