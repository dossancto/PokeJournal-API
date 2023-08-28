using PokeJournal.DTO;

namespace PokeJournal.Providers.TokenProvider; 

public interface ITokenProvider
{
  public string Hash(UserDTO user);
}

