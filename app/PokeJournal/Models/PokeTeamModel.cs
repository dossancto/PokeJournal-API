using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeJournal.Models;

public class PokeTeamModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public ICollection<PokemonListModel> Pokemons { get; set; } = default!;

    public Guid UserId;
    public UserModel User = default!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
