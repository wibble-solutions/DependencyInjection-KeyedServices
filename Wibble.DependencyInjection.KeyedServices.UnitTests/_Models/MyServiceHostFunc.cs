using System;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    internal class MyServiceHostFunc
    {
        public MyServiceHostFunc(Func<object, IMyService> services)
        {
            Factory = services;
        }
  
        public Func<object, IMyService> Factory { get; }
    }
}