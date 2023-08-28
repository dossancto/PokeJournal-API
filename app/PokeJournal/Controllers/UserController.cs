using Microsoft.AspNetCore.Mvc;
using PokeJournal.Models;
using PokeJournal.Data;
using PokeJournal.DTO;
using User = PokeJournal.Usecases.User;

namespace PokeJournal.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
  private readonly ApplicationDbContext _context;

  public UserController(ApplicationDbContext context){
    _context = context;
  }

  [HttpPost]
  public async Task<ActionResult<UserModel>> Register(UserDTO userDTO)
  {
      var user = new User.Register(_context, userDTO).Execute();

      return user;
  }
}
