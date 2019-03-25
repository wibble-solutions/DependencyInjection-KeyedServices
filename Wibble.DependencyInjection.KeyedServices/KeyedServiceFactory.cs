using System;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// A factory to create services based upon a key
    /// </summary>
    internal class KeyedServiceFactory : IKeyedServiceFactory
    {
        private readonly IKeyedServiceRegister _registrar;
        private readonly IServiceProvider _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedServiceFactory"/> class.
        /// </summary>
        /// <param name="registrar">The registrar.</param>
        /// <param name="services">The services.</param>
        public KeyedServiceFactory(IKeyedServiceRegister registrar, IServiceProvider services)
        {
            _registrar = registrar ?? throw new ArgumentNullException(nameof(registrar));
            _services  = services  ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Gets the instance of a service based upon a key.
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the service, or null if the key is not registered</returns>
        public object GetService(Type interfaceType, object key)
        {
            Type t = _registrar.LookUp(interfaceType, key);
            return t == null ? null : _services.GetService(t);
        }
    }
}