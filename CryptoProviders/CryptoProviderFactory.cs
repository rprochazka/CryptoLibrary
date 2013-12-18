using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace CryptoLibrary.CryptoProviders
{
    /// <summary>
    /// resolve proper use of the ICryptoProvider instance
    /// should be implemented as singleton
    /// </summary>
    internal sealed class CryptoProviderFactory : ICryptoProviderFactory
    {        
        /// <summary>
        /// mappings betweenn cryptoAliases and the corresponding implementors in the form of delegated
        /// </summary>
        private readonly Dictionary<string, Func<ICryptoProvider>> _cryptoProvidersMapping =
            new Dictionary<string, Func<ICryptoProvider>>();
 
        public CryptoProviderFactory()
        {
            InitCryptoProvidersMapping();
        }        

        public ICryptoProvider GetCryptoProvider(string cryptoAlias)
        {
            Func<ICryptoProvider> cryptoProvider;
            if (_cryptoProvidersMapping.TryGetValue(cryptoAlias, out cryptoProvider))
            {
                return cryptoProvider.Invoke();
            }

            throw new Exception("Unable to find eligible CryptoProvider");
        }
        
        /// <summary>
        /// initialize the mapping from the xml definition
        /// </summary>
        private void InitCryptoProvidersMapping()
        {            
            XDocument doc = XDocument.Load("Mappings/CryptoProvidersMapping.xml");
            var mappings =
                from m in doc.Descendants("mappings").Descendants("mapping")
                let xAttribute = m.Attribute("alias")
                let attribute = m.Attribute("type")
                where (xAttribute != null && attribute != null)
                select new
                           {
                               Alias = xAttribute.Value,
                               Implementor = attribute.Value
                           };

            foreach (var mapping in mappings)
            {
                var methodDelegate = CreateMethodDelegate(mapping.Implementor, "Create");
                if (null != methodDelegate)
                {
                    _cryptoProvidersMapping.Add(mapping.Alias, methodDelegate);
                }
            }                        
        }        

        //TODO - provide a lot of error handling: Focus on:
        // - TargetException (method does not exist), 
        // - TargetInvocationException (method exists, but rose an exc. when invoked), 
        // - TargetParameterCountException, 
        // - MethodAccessException (not the right privileges, happens a lot in ASP.NET), 
        // - InvalidOperationException (happens with generic types). 
        // You don't always need to try to catch all of them, it depends on the expected input and expected target objects.
        /// <summary>
        /// creates a delegate for the provided method in the provided class
        /// this solution is faster compared to reflection Activator.CreateInstance
        /// the current implementation support static method only
        /// </summary>
        /// <param name="className">here it's full path but for a safety reason it should better be a class name only
        /// with the namespace hardcoded
        /// </param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private Func<ICryptoProvider> CreateMethodDelegate(string className, string methodName)
        {
            // get the type (several ways exist, this is an eays one)
            //Type type = Type.GetType("CryptoLibrary.CryptoProviders." + className);
            var type = Type.GetType(className);            

            // works with public instance/static methods
            if (type != null)
            {
                MethodInfo mi = type.GetMethod(methodName);

                // the "magic", turn it into a delegate
                var createInstanceDelegate =
                    (Func<ICryptoProvider>) Delegate.CreateDelegate(typeof (Func<ICryptoProvider>), mi);

                return createInstanceDelegate;
            }

            return null;
        }

    }
}