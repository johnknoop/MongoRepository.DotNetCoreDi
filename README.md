# MongoRepository.DotNetCoreDi

Adds MongoRepository as a registered service with the .NET Core dependency resolver

## Install
    PM> Install-Package JohnKnoop.MongoRepository.DotNetCoreDi

## Configuration

In the `ConfigureServices` method of your `Startup` class, simply call `AddRepositories` on the service collection, and pass in your `IMongoClient` instance.

```csharp
public void ConfigureServices(IServiceCollection services)
    // Register IRepository<T>
    services.AddRepositories(mongoClient);

    // Register tenant key resolver
    services.AddScoped<ResolveTenantKey>(provider => () =>
    {
        // Pull the tenant key from wherever you keep it.
        // For example from user claim:

        var httpContextAccessor = provider.GetService<IHttpContextAccessor>();
        var user = httpContextAccessor.HttpContext?.User as ClaimsIdentity;
        return user?.Identity.Claims.SingleOrDefault(x => x.Type == "TenantKey")?.Value;
    });
}
```
Then you can accept `IRepository<AnyMappedType>` as constructor parameters.