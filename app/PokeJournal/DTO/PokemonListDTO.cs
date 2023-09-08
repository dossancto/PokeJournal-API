namespace PokeJournal.DTO;

public record PokemonListDTO(string? defaultName, string? customName, string? imgURL, Guid teamId, int pokemonIndex) { }
