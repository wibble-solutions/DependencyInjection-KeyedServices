using System;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Wibble.DependencyInjection.KeyedServices
{
    /// <summary>
    /// Extensions for the <see cref="IServiceCollection"/> interface
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Gets or sets the registrar.
        /// </summary>
        /// <value>
        /// The registrar.
        /// </value>
        public static IKeyedServiceRegistrar Registrar
        {
            get; set;
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <param name="services">The services</param>
        /// <param name="interfaceType">The type of the interface.</param>
        /// <param name="instanceType">The type of the service.</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void Add([NotNull]this IServiceCollection services, [NotNull] Type interfaceType, [NotNull] Type instanceType, [CanBeNull] object key)
        {
            GetRegistrar(services).Add(interfaceType, instanceType, key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void AddSingleton<TInterface, TService>([NotNull]this IServiceCollection services, object key)
            where TInterface : class
            where TService : class, TInterface
        {
            GetRegistrar(services).AddSingleton<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void AddTransient<TInterface, TService>([NotNull]this IServiceCollection services, object key)
            where TInterface : class
            where TService : class, TInterface
        {
            GetRegistrar(services).AddTransient<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void AddScoped<TInterface, TService>([NotNull]this IServiceCollection services, object key)
            where TInterface : class
            where TService : class, TInterface
        {
            GetRegistrar(services).AddScoped<TInterface, TService>(key);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services</param>
        /// <param name="key">The key.</param>
        /// <param name="implementationFactory">The implementation factory</param>
        [PublicAPI]
        public static void AddSingleton<TInterface, TService>([NotNull]this IServiceCollection services, object key, Func<IServiceProvider, TService> implementationFactory)
            where TInterface : class
            where TService : class, TInterface
        {
            GetRegistrar(services).AddSingleton<TInterface, TService>(key, implementationFactory);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services</param>
        /// <param name="key">The key.</param>
        /// <param name="implementationFactory">The implementation factory</param>
        [PublicAPI]
        public static void AddTransient<TInterface, TService>([NotNull]this IServiceCollection services, object key, Func<IServiceProvider, TService> implementationFactory)
            where TInterface : class
            where TService : class, TInterface
        {
            GetRegistrar(services).AddTransient<TInterface, TService>(key, implementationFactory);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services</param>
        /// <param name="key">The key.</param>
        /// <param name="implementationFactory">The implementation factory</param>
        [PublicAPI]
        public static void AddScoped<TInterface, TService>([NotNull]this IServiceCollection services, object key, Func<IServiceProvider, TService> implementationFactory)
            where TInterface : class
            where TService : class, TInterface
        {
            GetRegistrar(services).AddScoped<TInterface, TService>(key, implementationFactory);
        }

        /// <summary>
        /// Adds the specified mapping of a service using the key <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="services">The services</param>
        /// <param name="key">The key.</param>
        [PublicAPI]
        public static void Add<TInterface, TService>([NotNull]this IServiceCollection services, object key)
            where TService : class, TInterface
        {
            GetRegistrar(services).Add<TInterface, TService>(key);
        }

        /// <summary>
        /// Gets the registrar for the specified service container.
        /// </summary>
        /// <remarks>This is a singleton container, set up when this method is first called</remarks>
        /// <param name="serviceCollection">The service collection.</param>
        /// <returns>The keyed service registrar</returns>
        private static IKeyedServiceRegistrar GetRegistrar(IServiceCollection serviceCollection)
        {
            return Registrar ?? (Registrar = new KeyedServiceRegistrar(serviceCollection));
        }
    }
}