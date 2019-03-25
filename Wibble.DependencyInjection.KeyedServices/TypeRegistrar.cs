using System;
using System.Collections.Concurrent;
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
        }
    }
}