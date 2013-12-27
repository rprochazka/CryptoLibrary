namespace CryptoLibrary.CryptoProviders
{
    internal interface ICryptoProviderFactory
    {
        ICryptoProvider GetCryptoProvider(string cryptoAlias, string privateKey);
    }
}