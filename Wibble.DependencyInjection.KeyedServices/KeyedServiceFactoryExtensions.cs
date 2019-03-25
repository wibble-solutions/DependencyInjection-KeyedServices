using JetBrains.Annotations;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Extension methods for the <see cref="IKeyedServiceFactory"/> interface
    /// </summary>
    [PublicAPI]
    public static class KeyedServiceFactoryExtensions
    {
        /// <summary>
        /// Gets the instance of a service based upon a key.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="factory">The factory</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the service, or null if the key is not registered</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static TInterface GetService<TInterface>([NotNull]this IKeyedServiceFactory factory, [CanBeNull]object key)
        {
            return (TInterface) factory.GetService(typeof(TInterface), key);
        }

        /// <summary>
        /// Gets the instance of a service based upon a key.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="factory">The factory</param>
        /// <param name="key">The key.</param>
        /// <returns>The instance of the service, or null if the key is not registered</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static TInterface GetRequiredService<TInterface>([NotNull]this IKeyedServiceFactory factory, [CanBeNull]object key)
        {
            return (TInterface)factory.GetRequiredService(typeof(TInterface), key);
        }
    }
}