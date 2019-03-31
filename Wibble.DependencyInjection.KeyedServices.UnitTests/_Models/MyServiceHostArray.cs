using System.Collections.Generic;
using System.Linq;

namespace Wibble.DependencyInjection.KeyedServices.UnitTests
{
    internal class MyServiceHostArray
    {
        public MyServiceHostArray(IEnumerable<IMyService> services)
        {
            Services = services.ToArray();
        }

        public IMyService[] Services { get; }
    }
}