using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Extensions for the <see cref="IKeyedServiceRegistrar"/> interface 
    /// </summary>
    public static class KeyedServiceRegistrarExtensions
    {
        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="registrar">The registrar</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void AddSingleton<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key)
            where TService : class, TInterface
        {
            registrar.Services.AddSingleton<TService>();
            registrar.Add<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="registrar">The registrar</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void AddScoped<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key)
            where TService : class, TInterface
        {
            registrar.Services.AddScoped<TService>();
            registrar.Add<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="registrar">The registrar</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void AddTransient<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key)
            where TService : class, TInterface
        {
            registrar.Services.AddTransient<TService>();
            registrar.Add<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="registrar">The registrar</param>
        /// <param name="key">The key.</param>
        /// <param name="implementationFactory">The implementation factory</param>
        /// <param name="singleInstance">True if the service is a singe instance, otherwise false</param>
        [PublicAPI]
        public static void AddSingleton<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key, Func<IServiceProvider, TService> implementationFactory, bool singleInstance = false)
            where TService : class, TInterface
        {
            registrar.Services.AddSingleton(implementationFactory);
            registrar.Add<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="registrar">The registrar</param>
        /// <param name="key">The key.</param>
        /// <param name="implementationFactory">The implementation factory</param>
        /// <param name="singleInstance">True if the service is a singe instance, otherwise false</param>
        [PublicAPI]
        public static void AddScoped<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key, Func<IServiceProvider, TService> implementationFactory, bool singleInstance = false)
            where TService : class, TInterface
        {
            registrar.Services.AddScoped(implementationFactory);
            registrar.Add<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="registrar">The registrar</param>
        /// <param name="key">The key.</param>
        /// <param name="implementationFactory">The implementation factory</param>
        [PublicAPI]
        public static void AddTransient<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key, Func<IServiceProvider, TService> implementationFactory)
            where TService : class, TInterface
        {
            registrar.Services.AddTransient(implementationFactory);
            registrar.Add<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="registrar">The registrar</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void Add<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key)
            where TService : class, TInterface
        {
            registrar.Add(typeof(TInterface), typeof(TService), key);
        }

        /// <summary>
        /// Looks up a service via its key
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <param name="register">The register</param>
        /// <param name="key">The key.</param>
        /// <returns>The <see cref="Type"/> that implements <see cref="TInterface"/></returns>
        [PublicAPI]
        [MustUseReturnValue]
        public static Type LookUp<TInterface>([NotNull]this IKeyedServiceRegister register, [CanBeNull]object key)
        {
            return register.LookUp(typeof(TInterface), key);
        }
    }
}