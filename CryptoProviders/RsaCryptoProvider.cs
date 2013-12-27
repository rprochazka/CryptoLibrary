namespace CryptoLibrary.CryptoProviders
{
    internal class RsaCryptoProvider : ICryptoProvider
    {
        private readonly string _privateKey;
        private RsaCryptoProvider(string privateKey)
        {
            _privateKey = privateKey;
        }

        public string Encrypt(string input)
        {
            return "Encrypted using Rsa with private key: " + _privateKey;
        }

        public string Decrypt(string input)
        {
            return "Decypted using Rsa with private key:" + _privateKey;
        }

        public static ICryptoProvider Create(string privateKey)
        {
            return new RsaCryptoProvider(privateKey);
        }
    }
}