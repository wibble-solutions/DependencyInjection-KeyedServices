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
            where TInterface : class
            where TService : class, TInterface
        {
            registrar.Services.AddSingleton<TService>();
            registrar.Services.AddSingleton<TInterface, TService>(s => s.GetService<TService>());
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
            where TInterface : class
            where TService : class, TInterface
        {
            registrar.Services.AddTransient<TService>();
            registrar.Services.AddTransient<TInterface, TService>();
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
            where TInterface : class
            where TService : class, TInterface
        {
            registrar.Services.AddScoped<TService>();
            registrar.Services.AddScoped<TInterface, TService>();
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
        public static void AddSingleton<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key, Func<IServiceProvider, TService> implementationFactory)
            where TInterface : class
            where TService : class, TInterface
        {
            registrar.Services.AddSingleton(implementationFactory);
            registrar.Services.AddSingleton<TInterface, TService>(s => s.GetService<TService>());
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
            where TInterface : class
            where TService : class, TInterface
        {
            registrar.Services.AddTransient(implementationFactory);
            registrar.Services.AddTransient<TInterface, TService>(s => s.GetService<TService>());
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
        public static void AddScoped<TInterface, TService>([NotNull]this IKeyedServiceRegistrar registrar, object key, Func<IServiceProvider, TService> implementationFactory)
            where TInterface : class
            where TService : class, TInterface
        {
            registrar.Services.AddScoped(implementationFactory);
            registrar.Services.AddScoped<TInterface, TService>(s => s.GetService<TService>());
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
    }
}