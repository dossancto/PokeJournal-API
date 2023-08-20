namespace PokeJournal.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

public class UserModel{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id {get; set;}

    [Required]
    public string? UserName {get; set;}

    [EmailAddress]
    [Required]
    public string? Email {get; set;}

    [Required]
    public string? Password {get; set;}

    [Required]
    public string Salt {get; set;} = default!;

    public ICollection<PokeTeamModel> PokeTeams {get; set;} = default!;

    public DateTime CreatedAt {get; set;} = DateTime.Now;

    public DateTime UpdatedAt {get; set;} = DateTime.Now;
}
