namespace CryptoLibrary
{
    public interface ICryptoManager
    {
        string Encrypt(string input, string cryptoAlias);

        string Decrypt(string input, string cryptoAlias);
    }
}