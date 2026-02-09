using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Nadam.WarehouseManagement.DataAccess;

public static class Module
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WarehouseDbContext>((options) => {
            options.UseSqlServer(configuration.GetConnectionString("Warehouse"));
        });

        return services;
    }
}
