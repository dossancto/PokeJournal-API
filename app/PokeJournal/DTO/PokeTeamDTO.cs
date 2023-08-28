namespace PokeJournal.DTO;

public record PokeTeamDTO (string? teamId, int pokemonIndex, string? name, string? description, Guid? userId){}
