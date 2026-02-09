namespace Nadam.WarehouseManagement.Contracts.Models;

public record NewPartRequest(
    string Name,
    string Description,
    int Price,
    int Weight);
