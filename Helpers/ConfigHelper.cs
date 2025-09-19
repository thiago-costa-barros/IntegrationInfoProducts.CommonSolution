using CommonSolution.Entities.Common;
using CommonSolution.Entities.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommonSolution.Helpers
{
    public static class ConfigHelper
    {
        public const string ConnectionStringsSection = "ConnectionStrings";
        public static IServiceCollection AddCommonOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsoleLoggingOptions>(
                configuration.GetSection("ConsoleLoggingOptions"));

            services.Configure<ConnectionStringOptions>(
                configuration.GetSection(ConnectionStringsSection));

            services.Configure<DefaultUserService>(
                configuration.GetSection("DefaultUser"));

            services.Configure<LoggingOptions>(
                configuration.GetSection("LoggingOptions"));

            return services;
        }
    }
}
