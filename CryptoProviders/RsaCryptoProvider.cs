namespace CryptoLibrary.CryptoProviders
{
    internal class RsaCryptoProvider : ICryptoProvider
    {        
        public string Encrypt(string input)
        {
            return "Encrypted using Rsa";
        }

        public string Decrypt(string input)
        {
            return "Decypted using Rsa";
        }

        public static ICryptoProvider Create()
        {
            return new RsaCryptoProvider();
        }
    }
}