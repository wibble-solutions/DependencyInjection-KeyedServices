using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    /// <summary>
    /// Tests the <see cref="KeyedServiceRegistrarExtensions"/> class
    /// </summary>
    [TestFixture]
    public class TestKeyedServiceRegisterExtensions
    {
        [Test]
        public void LookUp_Key_DoesNotExist()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            Type t = register.LookUp<IMyService>("DOES_NOT_EXIST");

            // Assert
            Assert.That(t, Is.Null);
        }

        [Test]
        public void LookUp_Key_Exists()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            Type t = register.LookUp<IMyService>(AvailableServices.Service1);

            // Assert
            Assert.That(t, Is.EqualTo(typeof(MyService1)));
        }

        [Test]
        public void LookUp_DoesNotExist()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            var types = register.LookUp<IAsyncResult>();

            // Assert
            Assert.That(types, Is.Empty);
        }

        [Test]
        public void LookUp_Exists()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            var types = register.LookUp<IMyService>().ToList();

            // Assert
            Assert.That(types, Has.Count.EqualTo(2));
            Assert.That(types, Contains.Item(typeof(MyService1)));
            Assert.That(types, Contains.Item(typeof(MyService2)));
        }

        [Test]
        public void GetKeys()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            var types = register.GetKeys<IMyService>().ToList();

            // Assert
            Assert.That(types, Has.Count.EqualTo(4));
            Assert.That(types, Contains.Item(AvailableServices.Service1));
            Assert.That(types, Contains.Item(AvailableServices.Service2));
            Assert.That(types, Contains.Item("Service1"));
            Assert.That(types, Contains.Item("Service2"));
        }

        [Test]
        public void GetKeys_Typed_Enum()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            var types = register.GetKeys<IMyService, AvailableServices>().ToList();

            // Assert
            Assert.That(types, Has.Count.EqualTo(2));
            Assert.That(types, Contains.Item(AvailableServices.Service1));
            Assert.That(types, Contains.Item(AvailableServices.Service2));
        }

        [Test]
        public void GetKeys_Typed_String()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            var types = register.GetKeys<IMyService, string>().ToList();

            // Assert
            Assert.That(types, Has.Count.EqualTo(2));
            Assert.That(types, Contains.Item("Service1"));
            Assert.That(types, Contains.Item("Service2"));
        }

        [TestCase(AvailableServices.Service1, ExpectedResult = true)]
        [TestCase(AvailableServices.Service2, ExpectedResult = true)]
        [TestCase("Service1", ExpectedResult = true)]
        [TestCase("Service2", ExpectedResult = true)]
        [TestCase("DOES_NOT_EXIST", ExpectedResult = false)]
        public bool ContainsKey(object key)
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            // Assert
            return register.Contains<IMyService>(key);
        }

        [Test]
        public void Contains_True()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            bool contains = register.Contains<IMyService>();

            // Assert
            Assert.That(contains, Is.True);
        }

        [Test]
        public void Contains_False()
        {
            // Arrange
            IKeyedServiceRegister register = GetRegistrar();

            // Act
            bool contains = register.Contains<IAsyncResult>();

            // Assert
            Assert.That(contains, Is.False);
        }

        private static IKeyedServiceRegister GetRegistrar()
        {
            var registrar = new KeyedServiceRegistrar(new ServiceCollection());
            registrar.Add<IMyService, MyService1>(AvailableServices.Service1);
            registrar.Add<IMyService, MyService2>(AvailableServices.Service2);
            registrar.Add<IMyService, MyService1>("Service1");
            registrar.Add<IMyService, MyService2>("Service2");
            return registrar;
        }
    }
}