using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    /// <summary>
    /// Tests the <see cref="KeyedServiceRegistrar"/> class
    /// </summary>
    [TestFixture]
    public class TestKeyedServiceRegistrar
    {
        [Test]
        public void AddServices_ByName()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            var registrar = new KeyedServiceRegistrar(serviceCollection);
            registrar.AddTransient<IMyService, MyService1>("SERVICE1");
            registrar.AddTransient<IMyService, MyService2>("SERVICE2");

            var services = serviceCollection.BuildServiceProvider();

            // Act
            //// var factory = services.GetService<IKeyedServiceFactory>();
            var service1 = services.GetService<IMyService>("SERVICE1");
            var service2 = services.GetService<IMyService>("SERVICE2");

            // Assert
            Assert.That(service1, Is.InstanceOf<MyService1>());
            Assert.That(service2, Is.InstanceOf<MyService2>());
        }

        [Test]
        public void AddServices_ByEnum()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            var registrar = new KeyedServiceRegistrar(serviceCollection);
            registrar.AddTransient<IMyService, MyService1>(AvailableServices.Service1);
            registrar.AddTransient<IMyService, MyService2>(AvailableServices.Service2);

            var services = serviceCollection.BuildServiceProvider();

            // Act
            //// var factory = services.GetService<IKeyedServiceFactory>();
            var service1 = services.GetService<IMyService>(AvailableServices.Service1);
            var service2 = services.GetService<IMyService>(AvailableServices.Service2);

            // Assert
            Assert.That(service1, Is.InstanceOf<MyService1>());
            Assert.That(service2, Is.InstanceOf<MyService2>());
        }

    }

    public enum AvailableServices {
        Service1,
        Service2,
    }

    public interface IMyService
    {
    }

    public class MyService1 : IMyService
    {
    }

    public class MyService2 : IMyService
    {
    }
}
