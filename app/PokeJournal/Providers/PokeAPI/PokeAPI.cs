using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PokeJournal.Providers.PokeAPI; 

public class PokeAPI: IPokeAPIProvider {
    public readonly string BASE_URL = "https://pokeapi.co/api/v2";

    public async Task<PokemonResponse>? GetBasicInfos(string query){
      var url = $"{BASE_URL}/pokemon/{query}";
      using (HttpClient client = new HttpClient()){
        try
        {
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<PokemonResponse>(responseBody);

            return data;
        }
        catch (HttpRequestException ex)
        {
          return null;
        }
      }

    }
}