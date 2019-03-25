using System;
using Moq;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    [Flags]
    public enum InitMode
    {
        None = 0,
        Registrar = 1,
        Services = 2,
        CreateFactory = 4,
        All = CreateFactory | Registrar | Services
    }

    internal class TestUniverse : IDisposable
    {
        private readonly MockRepository _r = new MockRepository(MockBehavior.Strict);

        public TestUniverse(InitMode mode = InitMode.All)
        {
            Services = new Mock<IServiceProvider>(MockBehavior.Strict);
            Registrar = new Mock<IKeyedServiceRegister>(MockBehavior.Strict);

            if (mode.HasFlag(InitMode.CreateFactory))
            {
                CreateFactory(mode);
            }
        }

        public void CreateFactory(InitMode mode)
        {
            var actualServices  = mode.HasFlag(InitMode.Services) ? Services.Object : null;
            var actualRegistrar = mode.HasFlag(InitMode.Registrar) ? Registrar.Object : null;

            Factory = new KeyedServiceFactory(actualRegistrar, actualServices);
        }

        public KeyedServiceFactory Factory { get; set; }

        public Mock<IKeyedServiceRegister> Registrar { get; }

        public Mock<IServiceProvider> Services { get; }

        public void Dispose()
        {
            _r.VerifyAll();
        }
    }
}