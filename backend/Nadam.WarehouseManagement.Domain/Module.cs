using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Nadam.WarehouseManagement.Contracts;

namespace Nadam.WarehouseManagement.Domain;

public static class Module
{
    public static IServiceCollection AddWarehouse(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IWarehouse, Warehouse>();

        return services;
    }
}
