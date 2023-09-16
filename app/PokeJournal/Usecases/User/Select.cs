using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;

namespace PokeJournal.Usecases.User;

public class Select
{
    private readonly ApplicationDbContext _context;

    public Select(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<UserModel?> FromId(Guid userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);
        return user;
    }
}
