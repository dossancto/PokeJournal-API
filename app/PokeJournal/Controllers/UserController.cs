using Microsoft.AspNetCore.Mvc;
using PokeJournal.Models;
using PokeJournal.Data;
using PokeJournal.DTO;
using PokeJournal.Providers.TokenProvider;

using User = PokeJournal.Usecases.User;

namespace PokeJournal.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenProvider tokenProvider;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
        tokenProvider = new Jwt();
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<UserModel>> Register(UserDTO userDTO)
    {
        var user = await new User.Register(_context, userDTO).Execute();

        return user;
    }

    [HttpPost]
    [Route("Login")]
    public async Task<ActionResult<TokenDTO>> Login(UserDTO userDTO)
    {
        var user = await new User.Login(_context, userDTO.email, userDTO.password).Execute();
        var u = new UserDTO(user.Id, user.UserName ?? "", user.Email ?? "", "");
        var token = this.tokenProvider.Hash(u);

        return new TokenDTO(token, u);
    }
}
