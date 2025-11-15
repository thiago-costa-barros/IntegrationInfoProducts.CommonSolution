using CommonSolution.Domain.Entities.Common;
using CommonSolution.Domain.Entities.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommonSolution.Utils.Helpers
{
    public static class ConfigHelper
    {
        public static IServiceCollection AddCommonOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSettings>(
                configuration.GetSection("AppSettings"));

            services.Configure<ConnectionStringOptions>(
                configuration.GetSection("ConnectionStrings"));

            services.Configure<DefaultUserService>(
                configuration.GetSection("DefaultUser"));

            services.Configure<ConsoleLoggingOptions>(
                configuration.GetSection("ConsoleLoggingOptions"));

            services.Configure<LoggingOptions>(
                configuration.GetSection("LoggingOptions"));

            services.Configure<LoggingProvidersOptions>(
                configuration.GetSection("LoggingProvidersOptions"));

            return services;
        }
    }
}
