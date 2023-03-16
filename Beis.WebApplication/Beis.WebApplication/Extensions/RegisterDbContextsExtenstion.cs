using Beis.Common.Entities;
using Beis.Common.Entities.Interfaces;
using Beis.Common.Entities.Models;
using Beis.WebApplication.Data;
using Beis.WebApplication.Data.Entities;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Beis.WebApplication.Extensions
{
    public static class RegisterDbContextsExtenstion
    {
        const string ConnectionStringConfigurationKey = "CONNECTIONSTRING";
        public static void RegisterDbContexts(this WebApplicationBuilder builder)
        {
            var databaseConnectionString = builder.Configuration[ConnectionStringConfigurationKey];
            builder.Services.AddDbContext<WebApplicationDbContext<Applicant>>(options => options.UseNpgsql(databaseConnectionString));
            builder.Services.AddScoped<IBaseUserDbContext<BaseUserEntity>, WebApplicationDbContext<Applicant>>(); 

        }
    }
}
