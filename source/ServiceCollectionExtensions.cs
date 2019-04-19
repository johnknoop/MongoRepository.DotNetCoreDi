using System;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace JohnKnoop.MongoRepository.DotNetCoreDi
{
	public delegate string ResolveTenantKey();

	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services, IMongoClient mongoClient)
		{
			foreach (var entityType in MongoRepository.GetMappedTypes())
			{
				var genericRepositoryType = typeof(IRepository<>).MakeGenericType(entityType);

				services.AddTransient(genericRepositoryType, provider =>
				{
					var tenantKey = provider.GetService<ResolveTenantKey>()();
					var getRepositoryMethod = typeof(MongoConfiguration).GetMethod(nameof(MongoConfiguration.GetRepository));
					var getRepositoryMethodGeneric = getRepositoryMethod.MakeGenericMethod(entityType);

					return getRepositoryMethodGeneric.Invoke(null, new object[] { mongoClient, tenantKey });
				});	
			}

			return services;
		}
	}
}
