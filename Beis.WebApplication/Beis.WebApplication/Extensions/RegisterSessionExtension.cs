using Microsoft.AspNetCore.DataProtection;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Beis.WebApplication.Extensions
{
    public static class RegisterSessionExtension
    {
        public static IServiceCollection AddSession(this IServiceCollection services, string sessionName, int sessionTimeOutMinutes)
        {
            return services.AddSession(options =>
            {
                options.Cookie.Name = sessionName;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeOutMinutes);
            });
        }

        public static IServiceCollection AddRedisDistributedCache(this IServiceCollection services, string redisConnectionString, int sessionTimeOutMinutes)
        {
            if (string.IsNullOrWhiteSpace(redisConnectionString))
                throw new ArgumentNullException(nameof(redisConnectionString));

            var servicesResponse = services.AddSingleton(new DistributedCacheEntryOptions()
           .SetSlidingExpiration(TimeSpan.FromMinutes(sessionTimeOutMinutes)));

            var redis = ConnectionMultiplexer.Connect(redisConnectionString);

            servicesResponse.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            servicesResponse.AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");

            return servicesResponse;
        }
    }
}
