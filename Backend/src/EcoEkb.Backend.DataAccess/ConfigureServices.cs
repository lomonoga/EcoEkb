using EcoEkb.Backend.DataAccess.Domain.Services;
using EcoEkb.Backend.DataAccess.Domain.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Services.Interfaces;
using EcoEkb.Backend.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoEkb.Backend.DataAccess;

public static class ConfigureServices
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISendEmail, SendEmail>();
        services.AddScoped<ISecurityService, SecurityService>();
        services.AddSingleton<ITokenManager, JwtTokenManager>();
        services.AddSingleton<IHashService, HashService>();
        services.AddDbContext<EcoNotificationsDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("def_connection"));
        });
        services.AddScoped<EcoNotificationsDbContext>();
    }
}