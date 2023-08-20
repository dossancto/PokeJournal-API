using Microsoft.EntityFrameworkCore;
using PokeJournal.Models;

namespace PokeJournal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<PokemonListModel> PokemonLists { get; set; } = default!;
    public DbSet<PokeTeamModel> PokeTeams { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<PokemonListModel>()
            .HasOne(b => b.PokeTeam)
            .WithMany(a => a.Pokemons)
            .HasForeignKey(b => b.PokeTeamId);


    }
}
