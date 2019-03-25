using System;
using JetBrains.Annotations;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Provides access to registered keyed services
    /// </summary>
    [PublicAPI]
    public interface IKeyedServiceRegister
    {
        /// <summary>
        /// Looks up a service via its key
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="Type"/> that implements <see cref="interfaceType"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        Type LookUp([NotNull]Type interfaceType, [CanBeNull]object key);
    }
}