namespace PokeJournal.Usecases.User;

using PokeJournal.Models;
using PokeJournal.Data;
using PokeJournal.Providers.Criptografy;

public class Register{
    private readonly ApplicationDbContext _context;
    private readonly ICriptografyProvider crypto;
    private readonly UserModel user;

    public Register(ApplicationDbContext context, string name, string email, string password){
      _context = context;
      this.user = new UserModel {
        UserName = name,
        Email = email,
        Password = password
      };

      crypto = new Pbkdf2();
    }

    public UserModel Execute(){
      if (string.IsNullOrEmpty(user.Password)){
        throw new Exception("Password cant be blank");
      }

      (string hashedPassword, string salt) = crypto.HashPassword(user.Password);

      user.Password = hashedPassword;
      user.Salt = salt;

      var savedUser = _context.Users.Add(user);
      _context.SaveChanges();
      return savedUser.Entity;
    }
}
