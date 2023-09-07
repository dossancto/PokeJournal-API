using Microsoft.EntityFrameworkCore;

using PokeJournal.Data;
using PokeJournal.Models;
using PokeJournal.DTO;
using User = PokeJournal.Usecases.User;

namespace PokeJournal.Usecases.PokeTeam;

public class Create{
    private readonly ApplicationDbContext _context;
    private PokeTeamModel pokemonTeam;

    private readonly int pokemonIndex;
    private readonly string name;
    private readonly string description;

    public Create(ApplicationDbContext context, PokeTeamDTO dto): this(context, dto.pokemonIndex, dto.name, dto.description) {}
    public Create(ApplicationDbContext context, int pokemonIndex, string name): this(context, pokemonIndex, name, "") {}

    public Create(ApplicationDbContext context, int pokemonIndex, string name, string description){
      _context = context;
      this.pokemonIndex = pokemonIndex;

      this.name = name;
      this.description = description;
    }

    public async Task<Create> FromUserId(Guid userId){
      var user = await new User.Select(_context).FromId(userId);

      this.pokemonTeam = new PokeTeamModel {
        Name = name,
        Description = description,
        User = user
      };

      return this;
    }

    public Create FromUser(UserModel user){
      this.pokemonTeam = new PokeTeamModel {
        Name = name,
        Description = description,
        User = user
      };

      return this;
    }

    public async Task<PokeTeamModel> Execute(){
      if(this.pokemonTeam == null){
        throw new ArgumentNullException("use 'fromUser' or 'fromUserId' methods before call this one");
      }
      var saved = _context.PokeTeams.Add(this.pokemonTeam);
      await _context.SaveChangesAsync();
      var team = saved.Entity;

      await new PokeTeam.AddPokemon(_context, this.pokemonIndex, "", team).Execute();

      return team;
    }
}
