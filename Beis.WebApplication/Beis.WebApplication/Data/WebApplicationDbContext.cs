using Beis.Common.Entities.Models;
using Beis.Common.Entities;
using Microsoft.EntityFrameworkCore;


namespace Beis.WebApplication.Data
{
    public class WebApplicationDbContext<TEntity> : BaseUserDbContext<BaseUserEntity>
    {
        public WebApplicationDbContext()
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

    public WebApplicationDbContext(DbContextOptions options)
            : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        }

        public DbSet<Applicant> applicants { get; set; }
    }
}
