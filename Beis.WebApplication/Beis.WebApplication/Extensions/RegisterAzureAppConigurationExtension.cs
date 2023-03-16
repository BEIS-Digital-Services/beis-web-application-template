using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using System.Reflection;

namespace Beis.WebApplication.Extensions
{
    public static class RegisterAzureAppConfigurationExtension
    {
        const string AzureAppConfigConnectionStringKey = "AppConfig";


        public static void RegisterAzureAppConfiguration(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration(configBuilder =>
            {
                var connectionString = configBuilder.Build().GetConnectionString(AzureAppConfigConnectionStringKey);
                if (connectionString != null)
                {
                    configBuilder.AddAzureAppConfiguration(options =>
                    {
                        options
                            .Connect(connectionString)
                            .UseFeatureFlags(featureFlagOptions =>
                            {
                                featureFlagOptions.CacheExpirationInterval = TimeSpan.FromMinutes(5);
                            });
                    });
                }
            });
        }
    }
}