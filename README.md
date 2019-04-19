# MongoRepository.DotNetCoreDi

Adds MongoRepository as a registered service with the .NET Core dependency resolver

## Install
	PM> Install-Package JohnKnoop.MongoRepository.DotNetCoreDi

## Configuration

In the `ConfigureServices` method of your `Startup` class, simply call `AddRepositories` on the service collection, and pass in your `IMongoClient` instance.

```csharp
public void ConfigureServices(IServiceCollection services)
    services.AddRepositories(mongoClient);
}
```
Then you can accept `IRepository<AnyMappedType>` as constructor parameters.