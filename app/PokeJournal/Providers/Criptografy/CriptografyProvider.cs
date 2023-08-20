namespace PokeJournal.Providers.Criptografy;

interface ICriptografyProvider
{
    byte[] GenenateSalt();
    (string HashedPassword, string Password) Hash (string password, byte[] salt);
    (string HashedPassword, string Password) HashPassword (string password);
}
