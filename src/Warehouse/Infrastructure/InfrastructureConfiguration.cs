using Application.Common.Abstractions;
using Infrastructure.Services;
using Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureConfiguratione
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("WarehouseDB"));
        }
        else
        {
            var password = configuration.GetValue<string>("DATABASE_PASSWORD");
            var host = configuration.GetValue<string>("DATABASE_HOST");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    $"Username=postgres;Password={password};Host={host};Port=5432;Database=postgres;Pooling=true;Minimum Pool Size=0;Maximum Pool Size=100;Connection Lifetime=0;"));
        }

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}