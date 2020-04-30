using System;
using System.Collections.Generic;
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
        /// Looks up the type that provides a keyed service
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="Type"/> that implements <paramref name="interfaceType"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        Type LookUp([NotNull] Type interfaceType, [CanBeNull] object key);

        /// <summary>
        /// Looks up all of the types that provide a keyed service
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <returns>The <see cref="Type"/>s that implements <paramref name="interfaceType"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        IEnumerable<Type> LookUp([NotNull] Type interfaceType);

        /// <summary>
        /// Looks up all of the keys related with a keyed service
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <returns>The <see cref="Type"/>s that implements <paramref name="interfaceType"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        IEnumerable<object> GetKeys([NotNull] Type interfaceType);

        /// <summary>
        /// Determines whether the supplied keyed service exists
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <param name="key">The key, or null to determine whether there any are any keyed services registered</param>
        /// <returns>The <see cref="Type"/>s that implements <paramref name="interfaceType"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        bool Contains([NotNull] Type interfaceType, object key);

        /// <summary>
        /// Determines whether the supplied keyed service exists
        /// </summary>
        /// <param name="interfaceType">The interface type</param>
        /// <returns>The <see cref="Type"/>s that implements <paramref name="interfaceType"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        bool Contains([NotNull] Type interfaceType);
    }
}