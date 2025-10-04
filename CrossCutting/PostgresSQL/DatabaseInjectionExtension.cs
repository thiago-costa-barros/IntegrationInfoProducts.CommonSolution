using CommonSolution.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StackExchange.Redis;

namespace CommonSolution.CrossCutting.PostgresSQL
{
    public static class DatabaseInjectionExtension
    {
        public static IServiceCollection AddDatabaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPostgresConfig<ApplicationDbContext>();
            services.AddMongoConfig();
            services.AddRedisConfig();

            return services;
        }

        public static IServiceCollection AddPostgresConfig<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>((sp, opt) =>
            {
                var options = sp.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
                opt.UseNpgsql(options.PostgresConnection);
            });

            return services;
        }

        public static IServiceCollection AddMongoConfig(this IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
                return new MongoClient(options.MongoConnection);
            });

            return services;
        }

        public static IServiceCollection AddRedisConfig(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
                return ConnectionMultiplexer.Connect(options.RedisConnection);
            });

            return services;
        }
    }
}
