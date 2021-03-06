﻿using CryptoLibrary.CryptoProviders;

namespace CryptoLibrary
{
    public sealed class CryptoManager : ICryptoManager
    {
        private readonly ICryptoProviderFactory _providerFactory;

        public CryptoManager()
        {
            _providerFactory = new CryptoProviderFactory();
        }

        public string Encrypt(string input, string cryptoAlias)
        {            
            return _providerFactory.GetCryptoProvider(cryptoAlias, GetPrivateKey()).Encrypt(input);
        }

        public string Decrypt(string input, string cryptoAlias)
        {
            return _providerFactory.GetCryptoProvider(cryptoAlias, GetPrivateKey()).Decrypt(input);
        }

        private string GetPrivateKey()
        {
            return "pk1";
        }
    }
}
