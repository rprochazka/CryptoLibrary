namespace CryptoLibrary.CryptoProviders
{
    internal class ShaCryptoProvider : ICryptoProvider
    {
        private readonly string _privateKey;
        private ShaCryptoProvider(string privateKey)
        {
            _privateKey = privateKey;
        }

        public string Encrypt(string input)
        {
            return "Encrypted using Sha with private key: " + _privateKey;
        }

        public string Decrypt(string input)
        {
            return "Dencrypted using Sha with private key: " + _privateKey;
        }

        public static ICryptoProvider Create(string privateKey)
        {
            return new ShaCryptoProvider(privateKey);
        }
    }
}
