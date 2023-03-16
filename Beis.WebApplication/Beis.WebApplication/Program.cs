using Beis.Common.Interfaces;
using Beis.Common.Services;
using Beis.WebApplication.Data;
using Beis.WebApplication.Data.Entities;
using Beis.WebApplication.Extensions;
using Beis.WebApplication.Infrastructure;
using Beis.WebApplication.Interfaces;
using Beis.WebApplication.Extensions;
using FluentAssertions.Common;
using MediatR;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Serilog;
using StackExchange.Redis;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.WebPages;
using Beis.Common.Infrastructure;

const int SessionTimeOutMinutes = 30;
const int HstsMaxAgeDays = 365;

const string ConnectionStringConfigurationKey = "CONNECTIONSTRING";
const string AzureMonitorConfigurationKey = "AZURE_MONITOR_INSTRUMENTATION_KEY";
const string RedisConfigurationKey = "REDIS_PRIMARY_CONNECTION_STRING";



Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("BEIS Application Portal starting up");


try
{

    var builder = WebApplication.CreateBuilder(args);



    var telemetryKey = builder.Configuration[AzureMonitorConfigurationKey];
    if (telemetryKey != null)
        builder.Services.AddApplicationInsightsTelemetry(telemetryKey);

    builder.Host.ConfigureSerilog(telemetryKey, Program.Version);

    builder.Host.RegisterAzureAppConfiguration();

    builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>(string.Empty, LogLevel.Debug);
    builder.Logging.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);


    builder.Services.Configure<GoogleAnalyticsConfiguration>(builder.Configuration.GetSection("GoogleAnalytics"));


    builder.Services.AddControllersWithViews(config =>
    {
        config.Filters.Add<CookieFilter<WebApplicationDto>>();

        config.Filters.Add<SessionRequiredActionFilter>();

        // The navigation filter will automatically return to the action if there are validation errors in the viewmodel. This is not always desired
        // If you uncomment this then be sure to add any special cases to the controllersToIgnore list in the filter class.
        //config.Filters.Add<NavigationActionFilter>();
    });
    builder.Services.AddHttpContextAccessor();


    builder.Services.AddSession($"{nameof(WebApplication).Replace(".", "_").ToLower()}_session", SessionTimeOutMinutes);


    builder.Services.AddHsts(options =>
    {
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(HstsMaxAgeDays);
    });

    //var databaseConnectionString = builder.Configuration[ConnectionStringConfigurationKey];
    //builder.Services.AddDbContext<DbContext>(options => options.UseNpgsql(databaseConnectionString));
    builder.RegisterDbContexts();

    /*****  if using auto mapper create a handler and uncomment the code below *****/
    //builder.Services.AddAutoMapper(typeof(AbstractBaseHandler<,>).GetTypeInfo().Assembly,
    //    Assembly.GetExecutingAssembly());


    /*****  if using auto mapper create a command and uncomment the code below *****/
    //builder.Services.AddMediatR(Assembly.GetExecutingAssembly(),
    //    typeof(CreateApplicationCommand).GetTypeInfo().Assembly);

    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.RegisterOptions();

    #region redis
    var redisConnectionString = builder.Configuration[RedisConfigurationKey];
    if (!string.IsNullOrEmpty(redisConnectionString))
    {
        builder.Services.AddRedisDistributedCache(redisConnectionString, SessionTimeOutMinutes);

    }

    #endregion


    // Add services to the container. 

    builder.RegisterRepositories();
    builder.RegisterServices();





    var app = builder.Build();


    app.UseSerilogRequestLogging();


    if (builder.Configuration["ApplyEntityFrameworkMigrationsAtStartup"].AsBool() && !string.IsNullOrWhiteSpace(builder.Configuration[ConnectionStringConfigurationKey]))
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<Beis.WebApplication.Data.WebApplicationDbContext<Applicant>>();
            db.Database.Migrate();
        }
    }

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    app.UseStatusCodePagesWithRedirects("/Error?statusCode={0}");
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseSession();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{

    Log.Information($"BEIS {Assembly.GetExecutingAssembly().GetName()}  shut down complete");
    Log.CloseAndFlush();
}

// For integration testing and versioning
[ExcludeFromCodeCoverage]
public partial class Program
{
    public static readonly string Version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";
}