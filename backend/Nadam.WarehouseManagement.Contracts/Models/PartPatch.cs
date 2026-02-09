namespace Nadam.WarehouseManagement.Contracts.Models;

/// <summary>
/// Represents a patch for <see cref="Part"/> type.
/// </summary>
public record PartPatch(
    Guid Id,
    string Name,
    string Description,
    int Price,
    int Weight);
