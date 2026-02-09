using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Nadam.WarehouseManagement.Contracts.Interfaces;

namespace Nadam.WarehouseManagement.Domain;

public static class Module
{
    public static IServiceCollection AddWarehouse(this IServiceCollection services)
    {
        services.AddScoped<IWarehouse, Warehouse>();

        return services;
    }
}
