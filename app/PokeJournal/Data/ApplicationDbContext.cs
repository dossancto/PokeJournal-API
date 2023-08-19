using Microsoft.EntityFrameworkCore;
using PokeJournal.Models;

namespace PokeJournal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
