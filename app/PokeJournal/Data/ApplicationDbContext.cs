using Microsoft.EntityFrameworkCore;
using PokeJournal.Models;

namespace PokeJournal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<PokemonListModel> PokemonLists { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) { }
}
