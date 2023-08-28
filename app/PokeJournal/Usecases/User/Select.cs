using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.Providers.PokeAPI; 

namespace PokeJournal.Usecases.User;

public class Select{
    private readonly ApplicationDbContext _context;

    public Select(ApplicationDbContext context){
      _context = context;
    }

    public UserModel FromId(Guid userId){
      var user = _context.Users.FirstOrDefault(user => user.Id == userId);
      return user;
    }
}
