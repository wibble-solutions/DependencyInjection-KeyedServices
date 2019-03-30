using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Wibble.DependencyInjection.KeyedServices
{
    public partial class KeyedServiceRegistrar
    {
        /// <summary>
        /// Locates types by key
        /// </summary>
        /// <seealso cref="IKeyedServiceRegistrar" />
        private class TypeRegistrar
        {
            private readonly ConcurrentDictionary<object, Type> _types = new ConcurrentDictionary<object, Type>();

            /// <summary>
            /// Adds the specified key to type mapping.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <param name="serviceType">Type of the service.</param>
            public void Add(object key, [NotNull]Type serviceType)
            {
                _types[key] = serviceType;
            }

            /// <summary>
            /// Lookups the specified name.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns>The type that implements the requested service</returns>
            [MustUseReturnValue]
            public Type Lookup(object key)
            {
                return !_types.TryGetValue(key, out var type) ? default(Type) : type;
            }

            /// <summary>
            /// Determines whether the specified key exists.
            /// </summary>
            /// <param name="key">The key.</param>
            /// <returns>
            ///   <c>true</c> if the specified key exists; otherwise, <c>false</c>.
            /// </returns>
            public bool ContainsKey(object key)
            {
                return _types.ContainsKey(key);
            }

            /// <summary>
            /// Gets the keys.
            /// </summary>
            /// <returns>The keys</returns>
            /// <exception cref="NotImplementedException"></exception>
            public IEnumerable<Type> GetTypes()
            {
                return _types.Values.ToList();
            }

            /// <summary>
            /// Gets the keys.
            /// </summary>
            /// <returns>The keys</returns>
            /// <exception cref="NotImplementedException"></exception>
            public IEnumerable<object> GetKeys()
            {
                return _types.Keys.ToList();
            }
        }
    }
}