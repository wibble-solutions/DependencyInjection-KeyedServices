using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Registers services via a key
    /// </summary>
    [PublicAPI]
    public interface IKeyedServiceRegistrar
    {
        /// <summary>
        /// Gets the service collection.
        /// </summary>
        /// <value>
        /// The service collection.
        /// </value>
        [PublicAPI]
        IServiceCollection Services { get; }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <param name="interfaceType">The type of the interface.</param>
        /// <param name="instanceType">The type of the service.</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        void Add([NotNull]Type interfaceType, [NotNull]Type instanceType, [CanBeNull]object key);
    }
}