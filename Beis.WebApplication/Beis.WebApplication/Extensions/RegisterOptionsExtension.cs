using Beis.Common.Configuration;
using FluentAssertions.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace Beis.WebApplication.Extensions
{
    public static class RegisterOptionsExtension
    {
        public static void RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            services.Configure<CookieNamesConfiguration>(configuration.GetSection("CookieNamesConfiguration"));
            services.AddOptions<UrlOptions>().Bind(configuration).ValidateDataAnnotations();

            services.AddOptions<NotifyServiceSettings>().Bind(configuration).ValidateDataAnnotations();

            services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.All);
            services.Configure<CookiePolicyOptions>(options => options.Secure = CookieSecurePolicy.Always);

        }

        public static void RegisterOptions(this WebApplicationBuilder builder)
        {
            builder.Services.AddOptions();

            builder.Services.Configure<CookieNamesConfiguration>(builder.Configuration.GetSection("CookieNamesConfiguration"));
            builder.Services.AddOptions<UrlOptions>().Bind(builder.Configuration).ValidateDataAnnotations();

          
            builder.Services.AddOptions<NotifyServiceSettings>().Bind(builder.Configuration).ValidateDataAnnotations();

            builder.Services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.All);
            builder.Services.Configure<CookiePolicyOptions>(options => options.Secure = CookieSecurePolicy.Always);

        }
    }
}
