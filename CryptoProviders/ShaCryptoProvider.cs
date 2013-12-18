namespace CryptoLibrary.CryptoProviders
{
    internal class ShaCryptoProvider : ICryptoProvider
    {        
        public string Encrypt(string input)
        {
            return "Encrypted using Sha";
        }

        public string Decrypt(string input)
        {
            return "Dencrypted using Sha";
        }

        public static ICryptoProvider Create()
        {
            return new ShaCryptoProvider();
        }
    }
}
