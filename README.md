## DependencyInjection-KeyedServices

This package adds simple support for keyed services to an IServiceProvider. This is primarily intended to add support for keyed services to [Microsoft.Extensions.DependencyInjection.Abstractions](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection.Abstractions/) without relying on a custom DI container to provide that support.

## Purpose

In DotNet Core, it is possible to extend its stock Dependency Injection implemention with a fully fledged container (e.g. [Ninject](http://www.ninject.org/), [Autofac](https://autofac.org/)); when you have control over the container it is usually preferential to register keyed services with the underlying container. However in some circumstances, for example in third party libraries, you may not want to couple yourself to any particular DI container but still need keyed services. This package provides a way of registering and resolving keyed services without relying on any particular DI container.

## Getting Started

#### Creating a Registrar
Registration of keyed services is done using the ```KeyedServiceRegistrar``` class. This is the core service that holds all of the keyed service registrations for the application. This is used to register keyed service mappings and later resolve those types.

To create a keyed service registrar, pass the application's service collection to the constructor. This will create your keyed service registrar, and by default will also register this registrar, and its related interfaces, with the service collection so that they can be resolved later.

``` csharp
private IKeyedServiceRegistrar Create(IServiceCollection serviceCollection)
{
   IKeyedServiceRegistrar registrar = new KeyedServiceRegistrar(serviceCollection);
}
```
The service collection is now permanently associated with the keyed service registrar. This is best done right at the start of your application, before the application registers any services with the container.

You will usually have one ```IServiceCollection``` and one ```IKeyedServiceRegistrar``` for your whole application.

A key may be any type, a ```string```, an ```enum``` etc. They can be mixed up in any way, and you are not restricted to using a single key type with one interface.

#### Adding Keyed Services
Once you have created the registrar, its ```IKeyedServiceRegistrar``` interface can be used to add keyed services.

``` csharp
registrar.Add(typeof(MyInterface), typeof(MyClass), key);
```

This raw ```Add``` method adds the key mappings but does not register the type with the underlying container. You must do this yourself, or alternatively call ```AddSingleton```, ```AddTransient``` or ```AddScoped``` as shown below.

Keyed services can be added via the ```KeyedServiceRegistrar``` until the ```IServiceProvider``` is created by calling ```CreateServiceProvider``` on the underlying ```IServiceCollection```.

If you have a ```IKeyedServiceRegistrar``` you can add your key mappings and the underlying type registrations using these variants:

``` csharp
 registrar.AddSingleton<MyInterface, MyClass1>("MY_KEY_1");
 registrar.AddTransient<MyInterface, MyClass2>("MY_KEY_2");
 registrar.AddScoped<MyInterface, MyClass3>("MY_KEY_3");

 registrar.AddSingleton<MyInterface, MyClass4>("MY_KEY_4", s => { /* Factory delegate */ });
 registrar.AddTransient<MyInterface, MyClass5>("MY_KEY_5", s => { /* Factory delegate */ });
 registrar.AddScoped<MyInterface, MyClass6>("MY_KEY_6", s => { /* Factory delegate */ });
```

If you don't have a ```IKeyedServiceRegistrar```, and just have a ```IServiceCollection```,  you can use these extension methods which manage the ```IKeyedServiceRegistrar``` for you:

``` csharp
 serviceCollection.AddSingleton<MyInterface, MyClass7>("MY_KEY_7");
 serviceCollection.AddTransient<MyInterface, MyClass8>("MY_KEY_8");
 serviceCollection.AddScoped<MyInterface, MyClass9>("MY_KEY_9");

 serviceCollection.AddSingleton<MyInterface, MyClass10>("MY_KEY_10", s => { /* Factory delegate */ });
 serviceCollection.AddTransient<MyInterface, MyClass11>("MY_KEY_11", s => { /* Factory delegate */ });
 serviceCollection.AddScoped<MyInterface, MyClass12>("MY_KEY_12", s => { /* Factory delegate */ });
```

#### Getting Keyed Services
Keyed services can be found by resolving the ```IKeyedServiceFactory``` interface and getting the service using ```GetService``` or ```GetRequiredService``` and supplying the key.

``` csharp
MyInterface myKeyedService = (MyInterface)factory.GetService(typeof(MyInterface), "MY_KEY");
MyInterface myKeyedService = factory.GetService<MyInterface>("MY_KEY");
```

If you have a ```IServiceProvider```, you can use the following extension methods which resolve the ```IKeyedServiceFactory``` for you. Obviously there is a slight performance penalty for this.

``` csharp
MyInterface myService = (MyInterface)serviceProvider.GetService(typeof(MyInterface), "MY_KEY");
MyInterface myService = serviceProvider.GetRequiredService<MyInterface>("MY_KEY")
MyInterface myService = serviceProvider.GetRequiredService(typeof(MyInterface), "MY_KEY")
IEnumerable<MyInterface> services = serviceProvider.GetServices<MyInterface>();
```


#### Querying Keyed Services
You can query information about keyed services by getting and using the ```IKeyedServiceRegister``` interface. 

***Note:** ```KeyedServiceRegistrar``` implements ```IKeyedServiceRegister```.*

``` csharp
Type t1 = register.LookUp(typeof(MyInterface), "MY_KEY");
Type t2 = register.LookUp<MyInterface>("MY_KEY");

IEnumerable<Type> t3 = register.LookUp(typeof(MyInterface));
IEnumerable<Type> t4 = register.LookUp<MyInterface>();

IEnumerable<object> keys1 = register.GetKeys(typeof(MyInterface));
IEnumerable<object> keys2 = register.GetKeys<MyInterface>();
IEnumerable<string> keys3 = register.GetKeys<MyInterface, string>();

bool isAvailable1 = register.Contains(typeof(MyInterface), "MY_KEY");
bool isAvailable2 = register.Contains<MyInterface>("MY_KEY");

bool isAvailable3 = register.Contains(typeof(MyInterface));
bool isAvailable4 = register.Contains<MyInterface>();
```

#### Injecting Keyed Services

##### Injecting a particular keyed service into a constructor
You can get a particular keyed service into a class using a delegate during registration:

``` csharp
 serviceCollection.AddSingleton<MyInterface, MyClass>(s => new MyClass(s.GetService<IDependency>("KEY"))
```

##### Injection of an Enumerable of keyed services 
You can inject all keyed services as an ```IEnumerable<T>```:

``` csharp
internal class MyClass
{
	public MyClass(IEnumerable<MyInterface> types)
	{
	}
}
```

##### Injection of a Func Factory

If you wish to get a create a keyed service via a Func, you must first register your Func factory:

``` csharp
serviceCollection.AddSingleton<Func<object, MyInterface>>(s => s.GetService<MyInterface>);
```

and then you can inject this Func factory into your classes:

``` csharp
internal class MyClass
{
	private Func<object, MyInterface> _factory;

	public MyClass(Func<object, MyInterface> factory)
	{
		_factory = factory; // _factory(key) creates an instance of a keyed service
	}
}
```
