using System;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    /// <summary>
    /// Tests the <see cref="KeyedServiceRegistrarExtensions"/> class
    /// </summary>
    [TestFixture]
    public class TestKeyedServiceRegistrarExtensions
    {
        [Test]
        public void AddSingleton()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            registrar.AddSingleton<IMyService, MyService1>("MY_KEY");

            var provider = services.BuildServiceProvider();
            var svc1 = provider.GetService<IMyService>("MY_KEY");
            var svc2 = provider.GetService<MyService1>();

            // Assert
            Assert.That(svc1, Is.InstanceOf<MyService1>());
            Assert.That(svc1, Is.SameAs(svc2));
        }

        [Test]
        public void AddTransient()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            registrar.AddTransient<IMyService, MyService1>("MY_KEY");

            var provider = services.BuildServiceProvider();
            var svc1 = provider.GetService<IMyService>("MY_KEY");
            var svc2 = provider.GetService<MyService1>();

            // Assert
            Assert.That(svc1, Is.InstanceOf<MyService1>());
            Assert.That(svc1, Is.Not.SameAs(svc2));
        }

        [Test]
        public void AddScoped()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            registrar.AddScoped<IMyService, MyService1>("MY_KEY");

            var provider = services.BuildServiceProvider();
            var svc1 = provider.GetService<IMyService>("MY_KEY");
            var svc2 = provider.GetService<MyService1>();

            // Assert
            Assert.That(svc1, Is.InstanceOf<MyService1>());
            Assert.That(svc1, Is.SameAs(svc2));
        }

        [Test]
        public void AddSingleton_Delegate()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            var svc = new MyService1();
            registrar.AddSingleton<IMyService, MyService1>("MY_KEY", s => svc);

            var provider = services.BuildServiceProvider();
            var svc1 = provider.GetService<IMyService>("MY_KEY");
            var svc2 = provider.GetService<MyService1>();

            // Assert
            Assert.That(svc1, Is.SameAs(svc));
            Assert.That(svc1, Is.SameAs(svc2));
        }

        [Test]
        public void AddTransient_Delegate()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            var svc = new MyService1();
            registrar.AddTransient<IMyService, MyService1>("MY_KEY", s => svc);

            var provider = services.BuildServiceProvider();
            var svc1 = provider.GetService<IMyService>("MY_KEY");
            var svc2 = provider.GetService<MyService1>();

            // Assert
            Assert.That(svc1, Is.SameAs(svc));
            Assert.That(svc1, Is.SameAs(svc2));
        }

        [Test]
        public void AddScoped_Delegate()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            var svc = new MyService1();
            registrar.AddScoped<IMyService, MyService1>("MY_KEY", s => svc);

            var provider = services.BuildServiceProvider();
            var svc1 = provider.GetService<IMyService>("MY_KEY");
            var svc2 = provider.GetService<MyService1>();

            // Assert
            Assert.That(svc1, Is.SameAs(svc));
            Assert.That(svc1, Is.SameAs(svc2));
        }


        [Test]
        public void Add_LookUp()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            registrar.Add<IMyService, MyService1>("MY_KEY");
            Type t = registrar.LookUp<IMyService>("MY_KEY");

            // Assert
            Assert.That(t, Is.EqualTo(typeof(MyService1)));
        }

        [Test]
        public void LookUp_DoesNotExist()
        {
            // Arrange
            var services = new ServiceCollection();
            var registrar = new KeyedServiceRegistrar(services);

            // Act
            registrar.Add<IMyService, MyService1>("MY_KEY");
            Type t = registrar.LookUp<IMyService>("DOES_NOT_EXIST");

            // Assert
            Assert.That(t, Is.Null);
        }
    }
}