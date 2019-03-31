using System;
using NUnit.Framework;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    /// <summary>
    /// Tests the <see cref="KeyedServiceFactory"/> class
    /// </summary>
    [TestFixture]
    public class TestKeyedServiceFactory
    {
        [Test]
        public void Constructor_NullRegistrar()
        {
            using (var u = new TestUniverse(InitMode.None))
            {
                // Arrange

                // Act
                var ex = Assert.Throws<ArgumentNullException>(() => u.CreateFactory(InitMode.Services));

                // Assert
                Assert.That(ex.ParamName, Is.EqualTo("registrar"));
            }
        }

        [Test]
        public void Constructor_NullServices()
        {
            using (var u = new TestUniverse(InitMode.None))
            {
                // Arrange

                // Act
                var ex = Assert.Throws<ArgumentNullException>(() => u.CreateFactory(InitMode.Registrar));

                // Assert
                Assert.That(ex.ParamName, Is.EqualTo("services"));
            }
        }

        [Test]
        public void GetService_NotRegistered()
        {
            using (var u = new TestUniverse())
            {
                // Assert
                u.Registrar.Setup(r => r.LookUp(typeof(IMyService), "KEY"))
                           .Returns(default(Type));

                // Act
                var svc = u.Factory.GetService(typeof(IMyService), "KEY");

                // Assert
                Assert.That(svc, Is.Null);
            }
        }

        [Test]
        public void GetService_TypeNotRegistered()
        {
            using (var u = new TestUniverse())
            {
                // Arrange
                u.Registrar.Setup(r => r.LookUp(typeof(IMyService), "KEY"))
                           .Returns(typeof(MyService2));
                u.Services.Setup(s => s.GetService(typeof(MyService2))).Returns(default(MyService2));

                // Act
                var svc = u.Factory.GetService(typeof(IMyService), "KEY");

                // Assert
                Assert.That(svc, Is.Null);
            }
        }

        [Test]
        public void GetService_Ok()
        {
            using (var u = new TestUniverse())
            {
                // Arrange
                u.Registrar.Setup(r => r.LookUp(typeof(IMyService), "KEY"))
                    .Returns(typeof(MyService2));
                var svc = new MyService2();
                u.Services.Setup(s => s.GetService(typeof(MyService2))).Returns(svc);

                // Act
                var actualSvc = u.Factory.GetService(typeof(IMyService), "KEY");

                // Assert
                Assert.That(actualSvc, Is.SameAs(svc));
            }
        }

        [Test]
        public void GetRequiredService_NotRegistered()
        {
            using (var u = new TestUniverse())
            {
                // Assert
                u.Registrar.Setup(r => r.LookUp(typeof(IMyService), "KEY"))
                           .Returns(default(Type));

                // Act
                // ReSharper disable once MustUseReturnValue
                var ex = Assert.Throws<InvalidOperationException>(() => u.Factory.GetRequiredService(typeof(IMyService), "KEY"));

                // Assert
                Assert.That(ex.Message, Is.EqualTo("Service 'KEY' of type IMyService is not registered"));
            }
        }

        [Test]
        public void GetRequiredService_TypeNotRegistered()
        {
            using (var u = new TestUniverse())
            {
                // Arrange
                u.Registrar.Setup(r => r.LookUp(typeof(IMyService), "KEY"))
                    .Returns(typeof(MyService2));
                u.Services.Setup(s => s.GetService(typeof(MyService2))).Returns(default(MyService2));

                // Act
                // ReSharper disable once MustUseReturnValue
                var ex = Assert.Throws<InvalidOperationException>(() => u.Factory.GetRequiredService(typeof(IMyService), "KEY"));

                // Assert
                Assert.That(ex.Message, Is.EqualTo("Service 'KEY' of type IMyService is not registered"));
            }
        }

        [Test]
        public void GetRequiredService_Ok()
        {
            using (var u = new TestUniverse())
            {
                // Arrange
                u.Registrar.Setup(r => r.LookUp(typeof(IMyService), "KEY"))
                    .Returns(typeof(MyService2));
                var svc = new MyService2();
                u.Services.Setup(s => s.GetService(typeof(MyService2))).Returns(svc);

                // Act
                var actualSvc = u.Factory.GetRequiredService(typeof(IMyService), "KEY");

                // Assert
                Assert.That(actualSvc, Is.SameAs(svc));
            }
        }

    }
}