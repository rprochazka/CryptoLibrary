namespace CryptoLibrary.CryptoProviders
{
    internal interface ICryptoProvider
    {
        string Encrypt(string input);

        string Decrypt(string input);        
    }
}