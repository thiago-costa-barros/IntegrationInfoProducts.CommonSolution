using CommonSolution.Entities.Common;
using CommonSolution.Entities.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommonSolution.Helpers
{
    public static class ConfigHelper
    {
        public static IServiceCollection AddCommonOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsoleLoggingOptions>(
                configuration.GetSection("ConsoleLoggingOptions"));

            services.Configure<DefaultUserService>(
                configuration.GetSection("DefaultUser"));

            services.Configure<LoggingOptions>(
                configuration.GetSection("LoggingOptions"));

            services.Configure<ConnectionStringOptions>(
                configuration.GetSection("PostgresConnection"));

            services.Configure<ConnectionStringOptions>(
                configuration.GetSection("MongoConnection"));

            services.Configure<ConnectionStringOptions>(
                configuration.GetSection("RedisConnection"));

            return services;
        }
    }
}
