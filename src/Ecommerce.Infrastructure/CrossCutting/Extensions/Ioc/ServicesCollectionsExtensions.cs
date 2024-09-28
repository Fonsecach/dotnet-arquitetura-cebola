namespace Ecommerce.Infrastructure.CrossCutting.Extensions.Ioc;

public static class ServicesCollectionsExtensions
{
    public static IServiceCollection AddRavenDb(this IServiceCollection servicesCollection)
    {
        
        servicesCollection.TryAddSingleton<IDocumentStore>(ctx =>
        {
            var ravenDbSettings = ctx.GetRequiredService<IOptions<RavenDbStettings>>().Value;
            var store = new DocumentStore
            {
                Urls = new[] { ravenDbSettings.Url },
                Database = ravenDbSettings.DatabaseName
            };
            store.Initialize();
            return store;
        });

        return servicesCollection;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection servicesCollection)
    {
        servicesCollection.TryAddSingleton<ICustomerRepository, CustomerRepository>();
        return servicesCollection;
    }
}
