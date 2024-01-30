using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace mteams;
public static class StartupExtensions
{
    public static IServiceCollection AddConfiguration<T>(this IServiceCollection services, IConfiguration configuration)
        where T : class, new()
    {
        services.AddOptions<T>()
            .Bind(configuration)
            .ValidateDataAnnotations();
        services.Configure<T>(configuration);

        services.AddSingleton(resolver =>
        {
            var logger = resolver.GetService<ILogger<T>>();
            try
            {
                return resolver.GetRequiredService<IOptions<T>>().Value;
            }
            catch (OptionsValidationException ex)
            {
                foreach (var failure in ex.Failures)
                {
                    logger.LogCritical("Error validating configuration", failure);
                }

                throw;
            }
        });

        return services;
    }

    public static IServiceCollection AddCosmosClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(new CosmosClient(configuration.GetValue<string>("DocumentDBConnectionString"), new CosmosClientOptions()
        {
            SerializerOptions = new CosmosSerializationOptions()
            {
                PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
            }
        }));

        return services;
    }
}
