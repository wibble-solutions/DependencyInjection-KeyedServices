using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Extension methods for the <see cref="IKeyedServiceRegister"/> interface
    /// </summary>
    public static class KeyedServiceRegisterExtensions
    {
        /// <summary>
        /// Looks up a service via its key
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="register">The keyed service register</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="Type"/> that implements <typeparamref name="TInterface"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static Type LookUp<TInterface>(this IKeyedServiceRegister register, object key)
        {
            return register.LookUp(typeof(TInterface), key);
        }

        /// <summary>
        /// Looks up all of the types that implement a keyed service
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="register">The keyed service register</param>
        /// <returns>The <see cref="Type"/>s that implements the <typeparamref name="TInterface"/> keyed service</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static IEnumerable<Type> LookUp<TInterface>(this IKeyedServiceRegister register)
        {
            return register.LookUp(typeof(TInterface));
        }

        /// <summary>
        /// Looks up all of the keys related with a keyed service
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="register">The keyed service register</param>
        /// <returns>The <see cref="Type"/>s that implements <typeparamref name="TInterface"/> keyed service</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static IEnumerable<object> GetKeys<TInterface>(this IKeyedServiceRegister register)
        {
            return register.GetKeys(typeof(TInterface));
        }

        /// <summary>
        /// Looks up all of the keys related with a keyed service
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <param name="register">The keyed service register</param>
        /// <returns>The keys of the keyed services that implements <typeparamref name="TInterface"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static IEnumerable<TKey> GetKeys<TInterface, TKey>(this IKeyedServiceRegister register)
        {
            return register.GetKeys(typeof(TInterface)).OfType<TKey>();
        }

        /// <summary>
        /// Determines whether the supplied keyed service exists
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="register">The keyed service register</param>
        /// <param name="key">The key.</param>
        /// <returns>True if the supplied <typeparamref name="TInterface"/> the supplied keyed service registration, otherwise false</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static bool Contains<TInterface>(this IKeyedServiceRegister register, object key)
        {
            return register.Contains(typeof(TInterface), key);
        }

        /// <summary>
        /// Determines whether the supplied keyed service exists
        /// </summary>
        /// <typeparam name="TInterface">The interface type</typeparam>
        /// <param name="register">The keyed service register</param>
        /// <returns>True if the supplied <typeparamref name="TInterface"/> keyed service registrations, otherwise false</returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static bool Contains<TInterface>(this IKeyedServiceRegister register)
        {
            return register.Contains(typeof(TInterface));
        }
    }
}