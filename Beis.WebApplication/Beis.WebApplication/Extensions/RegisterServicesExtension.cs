using Beis.Common.Services;
using Beis.Common.Services.CompanyHouseApi;
using Beis.Common.Services.FCAServices;
using FluentAssertions.Common;

namespace Beis.WebApplication.Extensions
{
    public static class RegisterServicesExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            /***** if you are not using redis substitute the session service implementation below with Beis.Common.Services.CookieSessionService *****/
            services.AddSingleton<ISessionService, SessionService>();
            services.AddScoped<ICookieService, CookieService>();
        }
        public static void RegisterServices(this WebApplicationBuilder builder)
        {
            /***** if you are not using redis substitute the session service implementation below with Beis.Common.Services.CookieSessionService *****/
            builder.Services.AddSingleton<ISessionService, SessionService>();
            builder.Services.AddScoped<ICookieService, CookieService>();
         


            builder.Services.AddScoped<IApplicantService, ApplicantService>();
            builder.Services.AddScoped<IEmailVerificatoinNotifyService, EmailVerificatoinNotifyService>();
            builder.Services.AddScoped<IRestoreSessionService, RestoreSessionService>();

            builder.Services.AddSingleton<IRestClientFactory, RestClientFactory>();
          
        }


    }
}
