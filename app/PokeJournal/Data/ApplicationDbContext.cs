using Microsoft.EntityFrameworkCore;
using PokeJournal.Models;

namespace PokeJournal.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<PokemonListModel> PokemonLists { get; set; } = default!;
    public DbSet<PokeTeamModel> PokeTeams { get; set; } = default!;
    public DbSet<UserModel> Users { get; set; } = default!;
    public DbSet<FavoritePokemonModel> FavoritePokemons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonListModel>()
            .HasOne(a => a.PokeTeam)
            .WithMany(b => b.Pokemons)
            .HasForeignKey(a => a.PokeTeamId);

        modelBuilder.Entity<PokeTeamModel>()
            .HasOne(a => a.User)
            .WithMany(b => b.PokeTeams)
            .HasForeignKey(a => a.UserId);

        modelBuilder.Entity<FavoritePokemonModel>()
            .HasOne(a => a.User)
            .WithMany(b => b.FavoritePokemons)
            .HasForeignKey(a => a.UserId);
    }
}
