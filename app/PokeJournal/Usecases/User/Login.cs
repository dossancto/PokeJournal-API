namespace PokeJournal.Usecases.User;

using System.Text;

using PokeJournal.Models;
using PokeJournal.Data;
using PokeJournal.DTO;
using PokeJournal.Providers.Criptografy;

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

    public UserModel Execute(){
      string email = this.userdto.Email; 
      string password = this.userdto.Password;

      var storedUser = _context.Users.FirstOrDefault(user => user.Email == email);

      if(storedUser == null){
        throw new Exception($"User with email '{email}' not founded");
      }

      bool isCredentialsValid = crypto.VerifyPassword(password, storedUser.Password, storedUser.Salt);

      if (!isCredentialsValid){
        throw new Exception("Email or Password wrong");
      }

      return storedUser;
    }
}
