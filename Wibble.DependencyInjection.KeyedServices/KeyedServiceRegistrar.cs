using System;
using System.Collections.Concurrent;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Registers services via a key
    /// </summary>
    public partial class KeyedServiceRegistrar : IKeyedServiceRegistrar, IKeyedServiceRegister
    {
        private readonly ConcurrentDictionary<Type, TypeRegistrar> _registrations = new ConcurrentDictionary<Type, TypeRegistrar>();

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedServiceRegistrar"/> class.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="register">True to register the registrar and factory with the service collection</param>
        public KeyedServiceRegistrar([NotNull]IServiceCollection services, bool register = true)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));

            if (register)
            {
                services.AddSingleton<IKeyedServiceRegistrar>(s => this);
                services.AddSingleton<IKeyedServiceRegister>(s => this);
                services.AddSingleton<IKeyedServiceFactory, KeyedServiceFactory>();
            }
        }

        /// <summary>
        /// Gets the service collection.
        /// </summary>
        /// <value>
        /// The service collection.
        /// </value>
        public IServiceCollection Services { get; }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <param name="interfaceType">The type of the interface.</param>
        /// <param name="instanceType">The type of the service.</param>
        /// <param name="key">The key.</param>
        public void Add(Type interfaceType, Type instanceType, object key)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (instanceType == null)
            {
                throw new ArgumentNullException(nameof(instanceType));
            }

            var locator = _registrations.GetOrAdd(interfaceType, t => new TypeRegistrar());
            locator.Add(key, instanceType);
        }

        /// <summary>
        /// Looks up a service via its key
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="Type"/> that implements <see cref="interfaceType"/></returns>
        public Type LookUp(Type interfaceType, object key)
        {
            if (interfaceType == null)
            {
                throw new ArgumentNullException(nameof(interfaceType));
            }

            if (_registrations.TryGetValue(interfaceType, out var locator))
            {
                return locator.Lookup(key);
            }

            return null;
        }
    }
}
