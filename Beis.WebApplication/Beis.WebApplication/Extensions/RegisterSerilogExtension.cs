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
    public static class RegisterSerilogExtensions
    {

        public static void ConfigureSerilog(this ConfigureHostBuilder host, string? telemetryKey, string version)
        {
            host.UseSerilog((ctx, serviceProvider, lc) =>
            {
                lc.Enrich.FromLogContext();
                lc.Enrich.WithMachineName();
                lc.Enrich.WithEnvironmentUserName();
                lc.Enrich.WithExceptionDetails();
                lc.Enrich.WithProperty("Version", version);

                if (telemetryKey != null)
                {
                    lc.WriteTo.ApplicationInsights(serviceProvider.GetRequiredService<TelemetryConfiguration>(),
                        TelemetryConverter.Traces);
                }
            });
        }

    }
}