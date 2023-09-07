using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeJournal.Models;

public class PokemonListModel {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id {get; set;}

    [Required]
    public string? DefaultName {get; set;}

    [Required]
    public int PokemonIndex {get; set;}

    public string? CustomName {get; set;}

    public string? ImgURL {get; set;}

    public Guid PokeTeamId {get; set;}
    public PokeTeamModel? PokeTeam {get; set;}

    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}
