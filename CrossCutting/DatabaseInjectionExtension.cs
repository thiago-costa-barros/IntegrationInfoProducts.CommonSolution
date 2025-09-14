using CommonSolution.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StackExchange.Redis;

namespace CommonSolution.CrossCutting
{
    public static class DatabaseInjectionExtension
    {
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgresConfig<ApplicationDbContext>(configuration);
            services.AddMongoConfig();
            services.AddRedisConfig();

            return services;
        }

        public static IServiceCollection AddPostgresConfig<TDbContext>(this IServiceCollection services, IConfiguration configuration)
            where TDbContext : DbContext
        {
            var options = configuration.GetSection("Postgres").Get<ConnectionStringOptions>();
            services.AddDbContext<TDbContext>(opt => opt.UseNpgsql(options.DefaultConnectionString));
            return services;
        }

        public static IServiceCollection AddMongoConfig(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
                return new MongoClient(options.DefaultConnectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase(options.DefaultConnectionString);
            });

            return services;
        }

        public static IServiceCollection AddRedisConfig(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
                return ConnectionMultiplexer.Connect(options.DefaultConnectionString);
            });

            return services;
        }
    }
}
