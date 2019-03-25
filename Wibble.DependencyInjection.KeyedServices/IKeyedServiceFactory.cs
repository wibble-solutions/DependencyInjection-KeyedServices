using System;
using JetBrains.Annotations;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// A factory to create services based upon a key
    /// </summary>
    [PublicAPI]
    public interface IKeyedServiceFactory
    {
        /// <summary>
        /// Gets the instance of a service based upon a key.
        /// </summary>
        /// <param name="type">The type of object to create</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the service, or null if the key is not registered</returns>
        [PublicAPI]
        [MustUseReturnValue]
        object GetService([NotNull]Type type, [CanBeNull]object key);

        /// <summary>
        /// Gets the instance of a service based upon a key.
        /// </summary>
        /// <param name="type">The type of object to create</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the service, or null if the key is not registered</returns>
        [PublicAPI]
        [MustUseReturnValue]
        object GetRequiredService([NotNull]Type type, [CanBeNull]object key);
    }
}