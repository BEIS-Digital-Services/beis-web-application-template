using Beis.Common.Repositories.Interfaces;
using Beis.Common.Repositories;
using Beis.WebApplication.Data.Repositories;
using Beis.Common.Services;

namespace Beis.WebApplication.Extensions
{
    public static class RegisterRepositoriesExtension
    {
        public static void RegisterRepositories(this WebApplicationBuilder builder)
        {
         
            builder.Services.AddScoped<IBaseUsersRepository, BaseUsersRepository>();
            builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();
            
            builder.Services.AddScoped<IEmailVerificationService, EmailVerificationService>();

        }
    }
}
