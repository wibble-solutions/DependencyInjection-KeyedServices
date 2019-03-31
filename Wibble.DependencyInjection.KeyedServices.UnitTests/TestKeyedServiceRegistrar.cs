using System;
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
        public void Services_Null()
        {
            // Arrange
            // Act
            // ReSharper disable once ObjectCreationAsStatement
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => new KeyedServiceRegistrar(null));

            // Assert
            Assert.That(ex.ParamName, Is.EqualTo("services"));
        }

        [Test]
        public void RegistersTypes()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            var keyedSvcRegistrar = new KeyedServiceRegistrar(serviceCollection);
            var services = serviceCollection.BuildServiceProvider();

            // Act
            var registrar = services.GetService<IKeyedServiceRegistrar>();
            var register = services.GetService<IKeyedServiceRegister>();
            var factory = services.GetService<IKeyedServiceFactory>();

            // Assert
            Assert.That(registrar, Is.SameAs(keyedSvcRegistrar));
            Assert.That(register, Is.SameAs(keyedSvcRegistrar));
            Assert.That(factory, Is.InstanceOf<KeyedServiceFactory>());
        }

        [Test]
        public void Services()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            var keyedSvcRegistrar = new KeyedServiceRegistrar(serviceCollection);
            var actualServices = keyedSvcRegistrar.Services;

            // Assert
            Assert.That(actualServices, Is.SameAs(serviceCollection));
        }

        [Test]
        public void Add_InterfaceIsNull()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(serviceCollection);

            // Act
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => registrar.Add(null, typeof(MyService1), "KEY"));

            // Assert
            Assert.That(ex.ParamName, Is.EqualTo("interfaceType"));
        }

        [Test]
        public void Add_InstanceTypeIsNull()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(serviceCollection);

            // Act
            // ReSharper disable once ArgumentsStyleLiteral
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => registrar.Add(typeof(IMyService), instanceType: null, key: "KEY"));

            // Assert
            Assert.That(ex.ParamName, Is.EqualTo("instanceType"));
        }

        [Test]
        public void LookUp_InterfaceTypeIsNull()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(serviceCollection);

            // Act
            // ReSharper disable once MustUseReturnValue
            // ReSharper disable once AssignNullToNotNullAttribute
            var ex = Assert.Throws<ArgumentNullException>(() => registrar.LookUp(null, key: "KEY"));

            // Assert
            Assert.That(ex.ParamName, Is.EqualTo("interfaceType"));
        }

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
            var factory = services.GetService<IKeyedServiceFactory>();
            var service1 = factory.GetService<IMyService>("SERVICE1");
            var service2 = factory.GetService<IMyService>("SERVICE2");

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

        [Test]
        public void Resolve_Enumerable()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            var registrar = new KeyedServiceRegistrar(serviceCollection);
            registrar.Services.AddTransient<IMyService, MyService1>(AvailableServices.Service1);
            registrar.Services.AddTransient<IMyService, MyService2>(AvailableServices.Service2);
            serviceCollection.AddTransient<MyServiceHostArray>();
            var services = serviceCollection.BuildServiceProvider();

            // Act
            var host = services.GetService<MyServiceHostArray>();

            // Assert
            Assert.That(host.Services, Has.Length.EqualTo(2));
            Assert.That(host.Services[0], Is.InstanceOf<MyService1>());
            Assert.That(host.Services[1], Is.InstanceOf<MyService2>());
        }

        [Test]
        public void Resolve_Func()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            var registrar = new KeyedServiceRegistrar(serviceCollection);
            registrar.AddTransient<IMyService, MyService1>(AvailableServices.Service1);
            registrar.AddTransient<IMyService, MyService2>(AvailableServices.Service2);
            serviceCollection.AddTransient<MyServiceHostFunc>();
            serviceCollection.AddTransient<Func<object, IMyService>>(s => s.GetService<IMyService>);
            var services = serviceCollection.BuildServiceProvider();

            // Act
            var host = services.GetService<MyServiceHostFunc>();
            Assert.That(host.Factory, Is.Not.Null);
            var svc1 = host.Factory(AvailableServices.Service1);
            var svc2 = host.Factory(AvailableServices.Service2);

            // Assert
            Assert.That(svc1, Is.InstanceOf<MyService1>());
            Assert.That(svc2, Is.InstanceOf<MyService2>());
        }

    }
}
