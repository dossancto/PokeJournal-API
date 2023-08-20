namespace PokeJournal.Providers.PokeAPI; 

public interface IPokeAPIProvider
{
    Task<PokemonResponse>? GetBasicInfos(string query);
}
