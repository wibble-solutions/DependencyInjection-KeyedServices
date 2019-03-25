using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Extension methods for the <see cref="IServiceProvider"/> class
    /// </summary>
    [PublicAPI]
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets the instance of a service based upon a key.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="serviceProvider">The services</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the service, or null if the key is not registered</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static TInterface GetService<TInterface>([NotNull]this IServiceProvider serviceProvider, object key)
        {
            var factory = serviceProvider.GetService<IKeyedServiceFactory>();
            if (factory == null)
            {
                return default(TInterface);
            }

            return factory.GetService<TInterface>(key);
        }
    }
}