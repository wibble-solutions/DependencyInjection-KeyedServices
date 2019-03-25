using NUnit.Framework;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    /// <summary>
    /// Tests the <see cref="KeyedServiceFactoryExtensions"/> class
    /// </summary>
    [TestFixture]
    public class TestKeyedServiceFactoryExtensions
    {
        [Test]
        public void GetService()
        {
            using (var u = new TestUniverse())
            {
                // Arrange
                u.Registrar.Setup(r => r.LookUp(typeof(IMyService), "KEY"))
                    .Returns(typeof(MyService2));
                var svc = new MyService2();
                u.Services.Setup(s => s.GetService(typeof(MyService2))).Returns(svc);

                // Act
                var actualSvc = u.Factory.GetService<IMyService>("KEY");

                // Assert
                Assert.That(actualSvc, Is.SameAs(svc));
            }
        }
    }
}